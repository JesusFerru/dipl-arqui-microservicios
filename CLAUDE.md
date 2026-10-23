# diplom-arqui-microservicios

Repositorio del diplomado de Arquitectura con Microservicios. Contiene los
laboratorios de cada módulo y el proyecto final: el microservicio **Nurtricenter MS3**.

## Estructura

| Ruta | Contenido |
|---|---|
| `Nurtricenter/` | Microservicio MS3 — contratación, facturación y producción diaria. Es el único microservicio del repositorio. |
| `mod1/`, `mod2/`, `mod3/` | Laboratorios, apuntes y prácticas de cada módulo. |
| `PresentacionFinal-Grupo1/` | Material de la presentación final. |

## Nurtricenter MS3

Arquitectura por capas sobre .NET 8, con DDD y CQRS.

| Proyecto | Responsabilidad |
|---|---|
| `Nurtricenter.MS3.Core` | Agregados, value objects, eventos de dominio. Sin dependencias externas. |
| `Nurtricenter.MS3.Application` | Comandos, consultas y handlers de MediatR. |
| `Nurtricenter.MS3.Infrastructure` | EF Core sobre PostgreSQL, repositorios, unit of work. |
| `Nurtricenter.Api` | Endpoints de FastEndpoints, arranque y Swagger. |
| `Nurtricenter.MS3.Tests` | Pruebas unitarias. Usa mocks. |
| `Nurtricenter.MS3.IntegrationTests` | Pruebas de integración. Usa `WebApplicationFactory` y SQLite in-memory. |
| `Nurtricenter.MS3.PactTests.Consumer` | Pruebas de contrato, lado consumer. Genera `pacts/*.json` con PactNet. |
| `Nurtricenter.MS3.PactTests.Provider` | Pruebas de contrato, lado provider. Hostea la API real en un puerto TCP real y verifica el pacto. |

MS3 se comunica con MS1, MS2, MS4 y MS5, que no existen en este repositorio. Esas
integraciones están simuladas en `Nurtricenter.MS3.Application/Simulations/` con
identificadores deterministas, y los eventos salientes se registran en la tabla
`OutgoingIntegrationEvents` en lugar de publicarse en un bus.

## Comandos

Se ejecutan desde la raíz del repositorio.

```bash
dotnet build Nurtricenter/Nurtricenter.MS3.sln
dotnet test  Nurtricenter/Nurtricenter.MS3.sln
dotnet run   --project Nurtricenter/Nurtricenter.Api
```

`dotnet run` levanta la API en `http://localhost:8080` con Swagger, migra la base
y siembra datos de ejemplo. Requiere PostgreSQL en `localhost:5432`; la cadena de
conexión está en `Nurtricenter/Nurtricenter.Api/appsettings.json`.

Para levantar todo con Docker:

```bash
cd Nurtricenter && docker compose up --build
```

## Convenciones

- **Sin comentarios en el código**, salvo que sean estrictamente necesarios. El
  razonamiento no obvio va a la documentación del proyecto, no al archivo.
- Los mensajes de commit del historial están en inglés; el código, los nombres de
  pruebas y la documentación, en español.
- Los endpoints son `AllowAnonymous()`: la autenticación no está implementada.

## Pruebas unitarias

El entorno ya está construido y validado sobre `Nurtricenter.MS3.Tests`: Domain
(agregados y value objects de `Nurtricenter.MS3.Core`, sin mocks) y Application
(handlers de MediatR, con mocks de Moq sobre repositorios, `IUnitOfWork` e
`ISimulationService`). Para escribir o revisar pruebas, lee el skill
`unit-testing` (contrato del entorno, qué se mockea, comando de coverage) antes
de tocar nada.

Hay dos subagentes disponibles: `test-writer` para escribir las pruebas, y
`test-verifier` para auditarlas de forma independiente después. El verificador
no puede modificar archivos a propósito: reporta, y el escritor corrige.

## Pruebas de integración

El entorno ya está construido y validado. Para escribir o revisar pruebas, lee
los skills `integration-testing` (contrato del entorno) y `test-flows` (flujos de
negocio con rutas y códigos esperados) antes de tocar nada.

Hay dos subagentes disponibles: `integration-test-writer` para escribir las
pruebas, e `integration-test-verifier` para auditarlas de forma independiente
después. El verificador no puede modificar archivos a propósito: reporta, y el
escritor corrige.

Las pruebas de integración **no** se ejecutan con PostgreSQL. `ApiFactory`
sustituye el proveedor por SQLite in-memory, así que no hace falta Docker ni base
de datos para correrlas.

## Pruebas de contrato (Pact)

El entorno ya está construido y validado con PactNet 5.0.1: el consumer genera
un pacto real (`Nurtricenter.MS3.PactTests.Consumer/pacts/*.json`) y el
provider lo verifica contra la API real, hosteada en un puerto TCP real (no
`WebApplicationFactory` — PactNet no puede verificar contra un `TestServer` en
memoria). Antes de tocar nada, lee el skill `pact-testing`: tiene restricciones
no obvias del entorno (por ejemplo, una tilde en la descripción de una
interacción corrompe el matching de forma silenciosa).

Hay dos subagentes disponibles: `pact-writer` para escribir interacciones
nuevas en ambos lados, y `pact-checker` para auditarlas de forma independiente
después. El verificador no puede modificar archivos a propósito: reporta, y el
escritor corrige.

Tampoco requiere Docker ni PostgreSQL — el provider usa SQLite in-memory, igual
que `Nurtricenter.MS3.IntegrationTests`.
