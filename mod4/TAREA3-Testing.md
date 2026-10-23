# Tarea 3 — Contract Testing con Pact (Nurtricenter MS3)

Certifica el avance de la Actividad 3 (taller de Contract Testing) sobre el
caso de estudio del proyecto final, aplicado al microservicio **Nurtricenter
MS3**.

**Repositorio:** https://github.com/JesusFerru/dipl-arqui-microservicios
**Rama:** `feat/testing-2`
**Herramienta:** [PactNet 5.0.1](https://github.com/pact-foundation/pact-net) — la
versión oficial de Pact para .NET, para mantener el mismo lenguaje que el resto
del microservicio.

## Reglas

1. Aplicar el taller de Contract Testing al caso de estudio — avance, no
   necesariamente completo.
2. Al menos dos solicitudes realizadas desde el consumidor y verificadas desde
   el provider.
3. Crear un `pact-writer` con sus skills y un entorno validado de pruebas para
   IA.
4. Presentar un enlace al repositorio.

## 1. Consumer y Provider

Nurtricenter MS3 no tiene un frontend propio en este repositorio, así que el
contrato modela el caso real más directo: **un cliente HTTP de la API de
Contratos** (por ejemplo, un panel administrativo o un servicio aguas abajo que
necesita crear y consultar contratos) como consumer, y **la propia API de MS3**
como provider.

| Rol | Proyecto | Qué hace |
|---|---|---|
| Consumer | [`Nurtricenter.MS3.PactTests.Consumer`](Nurtricenter/Nurtricenter.MS3.PactTests.Consumer/) | Define las interacciones esperadas contra un mock server y genera el pacto |
| Provider | [`Nurtricenter.MS3.PactTests.Provider`](Nurtricenter/Nurtricenter.MS3.PactTests.Provider/) | Hostea la API real de MS3 (con SQLite en vez de PostgreSQL) en un puerto TCP real y verifica que cumple el pacto |

## 2. 2 requests

[`ContractsApiConsumerTests.cs`](Nurtricenter/Nurtricenter.MS3.PactTests.Consumer/ContractsApiConsumerTests.cs)
define dos interacciones sobre `/api/v1/contracts`:

| # | Solicitud | Provider state | Qué verifica |
|---|---|---|---|
| 1 | `POST /api/v1/contracts` | `el paciente y el plan existen` | Crea un contrato con paciente y plan válidos → 201, `status: "PendingPayment"` |
| 2 | `GET /api/v1/contracts/{id}` | `existe un contrato` (con el id como parámetro) | Consulta un contrato existente → 200 con su forma completa |

Ambas generan y verifican correctamente:

```bash
# Genera el pacto
dotnet test Nurtricenter/Nurtricenter.MS3.PactTests.Consumer/Nurtricenter.MS3.PactTests.Consumer.csproj
# Passed! - Failed: 0, Passed: 2, Skipped: 0, Total: 2

# Verifica el pacto contra la API real
dotnet test Nurtricenter/Nurtricenter.MS3.PactTests.Provider/Nurtricenter.MS3.PactTests.Provider.csproj
# Passed! - Failed: 0, Passed: 1, Skipped: 0, Total: 1
```

El pacto generado queda en
[`Nurtricenter.MS3.PactTests.Consumer/pacts/Nurtricenter Contracts Client-Nurtricenter MS3.json`](<Nurtricenter/Nurtricenter.MS3.PactTests.Consumer/pacts/Nurtricenter Contracts Client-Nurtricenter MS3.json>) —
committeado al repo, no es un artefacto de build: es el contrato en sí.

## 3. Cómo se verifica de verdad (no es un mock verificando un mock)

`Nurtricenter.MS3.PactTests.Provider` no reutiliza `WebApplicationFactory`
(PactNet no puede verificar contra un `TestServer` en memoria — su backend
nativo en Rust necesita un socket TCP real). En cambio,
[`PactProviderHost.cs`](Nurtricenter/Nurtricenter.MS3.PactTests.Provider/PactProviderHost.cs)
levanta la API real (`AddFastEndpoints`, `AddApplication`, `AddInfrastructure`,
handlers y agregados reales) en `http://127.0.0.1:9393`, sustituyendo
PostgreSQL por SQLite in-memory con la misma técnica que ya usa
`Nurtricenter.MS3.IntegrationTests`. El `provider state` `existe un contrato`
siembra un contrato real en esa base antes de que PactNet reproduzca la
solicitud `GET` contra el endpoint real — si el endpoint no devolviera lo que
el pacto promete, la verificación fallaría de verdad, no por construcción.

## 4. Entorno validado de pruebas de contrato para IA

Mismo patrón que las tareas anteriores:

| Pieza | Archivo |
|---|---|
| Skill — contrato del entorno y restricciones conocidas | [`.claude/skills/pact-testing/SKILL.md`](.claude/skills/pact-testing/SKILL.md) |
| Agente `pact-writer` — escribe interacciones en ambos lados y las ejecuta | [`.claude/agents/pact-writer.md`](.claude/agents/pact-writer.md) |
| Agente `pact-checker` — audita de forma independiente, sin permiso de escritura | [`.claude/agents/pact-checker.md`](.claude/agents/pact-checker.md) |

Documentado en [`CLAUDE.md`](CLAUDE.md#pruebas-de-contrato-pact).

## 5. Un hallazgo real del entorno (no solo del sistema)

Construir este entorno expuso un problema propio de la herramienta, no del
microservicio: con PactNet 5.0.1, una **tilde** en la descripción de una
interacción (`UponReceiving`) o en el nombre de un estado (`Given`) corrompe el
registro de la interacción en el backend nativo de forma silenciosa — el mock
server responde 500 "Request was not expected" a **cualquier** solicitud real,
sin importar si coincide. Se aisló de forma determinista probando variable por
variable hasta confirmar que la única diferencia entre una interacción que
fallaba siempre y una idéntica que pasaba siempre era la tilde en
"válidos" → "validos". Queda documentado en el skill `pact-testing` para no
volver a perder tiempo con esto.

## 6. Cómo reproducir todo

```bash
git clone https://github.com/JesusFerru/dipl-arqui-microservicios.git
cd dipl-arqui-microservicios
git checkout feat/testing-2

dotnet build Nurtricenter/Nurtricenter.MS3.sln
dotnet test  Nurtricenter/Nurtricenter.MS3.PactTests.Consumer/Nurtricenter.MS3.PactTests.Consumer.csproj
dotnet test  Nurtricenter/Nurtricenter.MS3.PactTests.Provider/Nurtricenter.MS3.PactTests.Provider.csproj
```

No requiere Docker ni PostgreSQL.

## Estado combinado de la suite 

```bash
dotnet test Nurtricenter/Nurtricenter.MS3.sln
```

```
Nurtricenter.MS3.Tests.dll:                88 passed, 0 failed   (Tarea 1)
Nurtricenter.MS3.IntegrationTests.dll:     18 passed, 0 failed   (Tarea 2)
Nurtricenter.MS3.PactTests.Consumer.dll:    2 passed, 0 failed   (Tarea 3)
Nurtricenter.MS3.PactTests.Provider.dll:    1 passed, 0 failed   (Tarea 3)
Total: 109 passed, 0 failed
```

## Notas

- Solo se cubrió el recurso de Contratos.
- El provider corre en un puerto fijo (`9393`). Si ese puerto está ocupado en
  la máquina donde se corre, `PactProviderHost.ServerUri` hay que cambiarlo a
  manual — no se implementó asignación dinámica de puerto para mantener el
  ejemplo simple, siguiendo el mismo patrón que usa el propio repositorio
  oficial de `pact-net` en su documentación.
