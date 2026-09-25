# Presentación Final — MS Nur TriCenter

`docker-compose.yml` en esta carpeta integra (`include:`) el `docker-compose.yml`
de cada microservicio del equipo, cada uno en su propia subcarpeta con su
`Dockerfile` y su `docker-compose.yml`.

## Microservicios incluidos

| Carpeta | Microservicio | Integrante | Puerto |
|---|---|---|---|
| `ms1-gestion-clinica-nutricional/` | MS1 — Gestión Clínica Nutricional | Diego Cabrera Alvarez | 3001 |
| `ms2-plans-and-recipes-catalog/` | MS2 — Plans & Recipes Catalog | Robert Perez Lino | 8082 (API) / 5433 (DB) |
| `ms3-contracting-production/` | MS3 — Contracting & Production | Luis Jesus Ferrufino Burgos | 8083 |
| `ms4-delivery-schedule-management/` | MS4 — Delivery Schedule Management | Franklin Andre Romero Padilla | 8081 (API) / 5432 (DB) |
| `ms5-logistics-and-delivery/` | MS5 — Logistics & Delivery | Andres Sebastian Guerrero Camacho | 8080 |

## Imágenes en Docker Hub

| Microservicio | Docker Hub |
|---|---|
| MS1 | https://hub.docker.com/r/logfile1995/gestionclinicanutricional |
| MS2 | https://hub.docker.com/r/robertperezlino/ms2-plans-and-recipes-catalog |
| MS3 | https://hub.docker.com/r/jesusferru/nurtricenter-ms3 |
| MS4 | https://hub.docker.com/r/franklin120/delivery-schedule-management |
| MS5 | https://hub.docker.com/repository/docker/aguerreroc/nurtricenter-ms5/general |

## Requisitos previos

- Docker + Docker Compose v2 (con soporte de la directiva `include:`).
- Los microservicios **MS3** y **MS5** se conectan a bases hosteadas (Neon.tech)
  y necesitan su propio archivo `.env` en su subcarpeta (gitignoreado) con la
  connection string correspondiente.

## Cómo levantar todo el stack

```bash
docker compose up -d     # baja las imágenes desde Docker Hub y levanta todo
docker compose ps        # verifica que los 8 contenedores estén Up/healthy
```

Levanta los 5 microservicios (más sus bases de datos/cache locales) en una red
compartida (`backend`).

## URLs de acceso

| Microservicio | URL |
|---|---|
| MS1 | http://localhost:3001/swagger/index.html |
| MS2 | http://localhost:8082/swagger/v1/swagger.json |
| MS3 | http://localhost:8083/swagger  (health: `/health`) |
| MS4 | http://localhost:8081/swagger/index.html |
| MS5 | http://localhost:8080/health |

## Detener todo

```bash
docker compose down       # detiene y borra los contenedores
docker compose down -v    # además borra los volúmenes de las DBs locales (MS2, MS4)
```

## Notas

- Todos los microservicios se distribuyen como imagen en Docker Hub y se
  referencian con `image:` en su compose (no se necesita el código fuente).
- **MS1, MS3 y MS5** usan bases de datos externas/hosteadas (su conexión va por
  `environment:` o `.env`, sin contenedor de DB local).
- **MS2 y MS4** levantan su propia base local (Postgres) dentro del stack, y
  **MS4** además usa Redis.
- Todos comparten la red `backend` para comunicarse entre sí por nombre de
  servicio.
