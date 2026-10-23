# Lab 4 — Docker Compose: api + db + cache

Práctica de Docker Compose con **3 servicios** (API de catálogo de productos,
PostgreSQL y Redis), healthchecks, `depends_on` con condición, verificación de
red/DNS y escalado horizontal.

## Estructura

```
lab4/
├── Catalog.Api/             # API .NET 8 (Minimal API + EF Core Npgsql + Redis)
│   ├── Catalog.Api.csproj
│   ├── Program.cs           # endpoints, DbContext, seed
│   ├── Product.cs           # entidad (Id, Name, Price)
│   └── appsettings.json
├── Dockerfile               # multi-stage sdk:8.0 -> aspnet:8.0 + curl
├── .dockerignore
├── docker-compose.yml       # servicios api, db, cache
└── README.md
```

## Servicios

| Servicio | Imagen | Puerto host | Rol | Healthcheck |
|---|---|---|---|---|
| `api` | `jesusferru/catalog-api:1.0.0` (Docker Hub) | 8080–8082 (rango) | Catálogo de productos | `curl /health` |
| `db` | `postgres:16-alpine` | 5432 | PostgreSQL (Npgsql) | `pg_isready` |
| `cache` | `redis:7-alpine` | 6379 | Redis (cache distribuido) | `redis-cli ping` |

Todos comparten la red `backend` y se referencian por **nombre de servicio**
(`db`, `cache`), usando el DNS embebido de Docker.

> **Imagen en Docker Hub**: la api se baja de `jesusferru/catalog-api:1.0.0`
> (https://hub.docker.com/r/jesusferru/catalog-api). El `Dockerfile` se conserva
> como referencia de cómo se construyó.

> **Por qué la api usa un rango de puertos** (`8080-8082:8080`): para poder
> escalar. Cada réplica toma el siguiente puerto del rango. Con un único puerto
> fijo (`8080:8080`) el escalado falla (ver requisito 4).

## La API (Catálogo de productos)

- **.NET 8 Minimal API** + **EF Core Npgsql** + **Redis** (`IDistributedCache`).
- Endpoints:
  - `GET /health` → verifica conexión a Postgres.
  - `GET /products` → **cache-aside**: Redis → si no, lee Postgres, cachea 30s.
  - `POST /products` → guarda en Postgres e **invalida el cache**.
  - `GET /info` → devuelve el hostname del contenedor (para el round-robin).
- Swagger UI en `/swagger/index.html`.
- En `Development`: `EnsureCreated()` crea el schema y siembra 2 productos.

> La base **se crea sola** al levantar: `POSTGRES_DB: catalog` crea la base vacía
> y la API crea las tablas + seed. No hay ningún script SQL manual.

## Requisitos cumplidos

### 1. Compose con 3 servicios
`docker-compose.yml` declara `api`, `db` y `cache`.

### 2. Healthchecks y depends_on con condition
- Cada servicio tiene `healthcheck`.
- `api` usa `depends_on` con `condition: service_healthy` sobre `db` y `cache`,
  así **no arranca hasta que ambos estén `healthy`**.

### 3. Verificar red, healthchecks, DNS
- `docker compose ps` → los 3 en estado `(healthy)`.
- `docker compose exec api getent hosts db` → resuelve `db` a su IP interna.
- `docker compose exec api getent hosts cache` → idem para `cache`.

### 4. Escalar un servicio y ver qué pasa
- **Gotcha**: con un puerto fijo (`8080:8080`), `--scale api=3` **falla**:
  `Bind for 0.0.0.0:8080 failed: port is already allocated` (las réplicas no
  pueden mapear el mismo puerto del host).
- **Solución**: usar un **rango** (`8080-8082:8080`). Cada réplica toma un puerto
  distinto (8080, 8081, 8082) y el DNS hace round-robin.

## Comandos (en orden)

```bash
cd mod3/lab4

# 1. Levantar (baja la imagen de Docker Hub si no la tenés)
docker compose up -d

# 2. Estado (deben quedar "healthy")
docker compose ps

# 3. Probar la API
curl http://localhost:8080/health
curl http://localhost:8080/products
curl -X POST http://localhost:8080/products \
     -H "Content-Type: application/json" \
     -d '{"name":"Keyboard","price":49.99}'
curl http://localhost:8080/info
# Swagger: http://localhost:8080/swagger/index.html

# 4. Verificar DNS (nombre de servicio -> IP interna)
docker compose exec api getent hosts db
docker compose exec api getent hosts cache

# 5. Escalar a 3 réplicas (el rango de puertos lo permite)
docker compose up -d --scale api=3
docker compose ps                             # api-1, api-2, api-3
docker compose exec api getent hosts api       # 3 IPs (round-robin)
for p in 8080 8081 8082; do curl http://localhost:$p/info; done

# 6. Volver a 1 réplica
docker compose up -d --scale api=1

# 7. Bajar
docker compose down       # mantiene el volumen (datos)
docker compose down -v    # borra también los datos
```

## Resultados observados

- `docker compose ps`: los 3 servicios quedan `Up (healthy)`; `api` espera a
  `db` y `cache` gracias a `depends_on: condition: service_healthy`.
- `getent hosts db` → `172.x.x.x db`; `getent hosts cache` → `172.x.x.x cache`.
- `--scale api=3` con puerto fijo → error `port is already allocated`.
- `--scale api=3` con rango `8080-8082:8080` → 3 réplicas, `getent hosts api`
  devuelve 3 IPs y cada puerto responde con un hostname distinto (round-robin).
