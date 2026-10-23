# Tarea 1 — Unit Testing (Nurtricenter MS3)

Certifica el avance de la Actividad 1 (taller de Unit Tests) sobre el caso de
estudio del proyecto final, aplicado al microservicio **Nurtricenter MS3**.

**Repositorio:** https://github.com/JesusFerru/dipl-arqui-microservicios
**Rama:** `feat/testing`
**Lenguaje:** C# / .NET 8 (el mismo seleccionado para el microservicio desde el
Módulo 2).

## Qué pedía la actividad

1. Aplicar el taller de Unit Tests al caso de estudio — no hace falta que esté
   completo, pero debe reflejar comprensión de los conceptos (mocks, code
   coverage).
2. Continuar con el lenguaje ya seleccionado para el microservicio.
3. Crear un `test-writer` con sus skills y un entorno validado de pruebas para
   IA.
4. Presentar un enlace al repositorio.

## 1. Unit tests aplicados

Suite en [`Nurtricenter/Nurtricenter.MS3.Tests/`](Nurtricenter/Nurtricenter.MS3.Tests/),
con xUnit + FluentAssertions + Moq:

| Carpeta | Qué prueba | Archivos |
|---|---|---|
| `Domain/` | Agregados, entidades y value objects de `Nurtricenter.MS3.Core` — `Contract`, `ControlCharge`, `DailyProductionOrder`, `Package`, `Invoice`, `Label`, `QualityValidation`, `ProductionItem` | 6 clases de prueba |
| `Application/` | Handlers de MediatR de `Nurtricenter.MS3.Application` — contratación, facturación, producción y paquetes | 4 clases de prueba |

```bash
dotnet test Nurtricenter/Nurtricenter.MS3.Tests/Nurtricenter.MS3.Tests.csproj
```

```
Passed!  - Failed: 0, Passed: 88, Skipped: 0, Total: 88
```

## 2. Mocks

Las pruebas de `Application/` mockean con **Moq** las dependencias externas de
cada handler — nunca el handler ni las entidades de dominio que construye:

- Repositorios: `IContractRepository`, `IControlChargeRepository`,
  `IDailyProductionOrderRepository`, `IPackageRepository`,
  `IOutgoingEventRepository`.
- `IUnitOfWork` (confirma `CommitAsync` con `Times.Once`).
- `ISimulationService` (simula las respuestas de MS1/MS2 sin depender de la
  implementación real).

Ejemplo real ([`ContractHandlersTests.cs`](Nurtricenter/Nurtricenter.MS3.Tests/Application/ContractHandlersTests.cs)):

```csharp
private readonly Mock<IContractRepository> _repository = new();
private readonly Mock<IUnitOfWork> _unitOfWork = new();
private readonly Mock<ISimulationService> _simulation = new();
...
_repository.Verify(r => r.AddAsync(It.IsAny<Contract>()), Times.Once);
_unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
```

Las pruebas de `Domain/` **no** usan mocks a propósito: los agregados se
prueban en aislamiento puro, invocando sus métodos reales (`Contract.Create(...)`,
`.ProcessPayment(...)`, `.CancelContract(...)`), que es la forma correcta de
probar lógica de dominio sin infraestructura.

## 3. Code coverage

Medido con `coverlet.collector` sobre la suite de `Nurtricenter.MS3.Tests`:

```bash
dotnet test Nurtricenter/Nurtricenter.MS3.Tests/Nurtricenter.MS3.Tests.csproj \
  --collect:"XPlat Code Coverage" \
  --results-directory Nurtricenter/Nurtricenter.MS3.Tests/TestResults
```

| Paquete | Line coverage | Branch coverage | Por qué |
|---|---|---|---|
| `Nurtricenter.MS3.Core` (Domain) | **89.0%** | 83.3% | Lógica de negocio pura — es el foco de esta actividad |
| `Nurtricenter.MS3.Application` (Handlers) | **68.8%** | 85.4% | Los 11 handlers están al ~100%; lo que baja el promedio son DTOs (records sin comportamiento) y el registro de DI, que no se prueban por unidad |
| `Nurtricenter.MS3.Api` / `Infrastructure` | 0% (esperado) | — | No son objetivo de unit tests: se prueban con HTTP y base de datos real (SQLite in-memory) en `Nurtricenter.MS3.IntegrationTests` (15 pruebas, ver Tarea 2) |

La actividad no exige un mínimo de cobertura (eso llega en el Proyecto Final,
con el 80%); este número certifica el avance real medido, no una estimación.

## 4. Entorno validado de pruebas para IA

Igual que se hizo para integración (Tarea 2), se agregó un entorno propio para
generar y auditar pruebas unitarias con IA:

| Pieza | Archivo |
|---|---|
| Skill — contrato del entorno (dónde va cada prueba, qué se mockea, comandos) | [`.claude/skills/unit-testing/SKILL.md`](.claude/skills/unit-testing/SKILL.md) |
| Agente `test-writer` — escribe y ejecuta pruebas unitarias | [`.claude/agents/test-writer.md`](.claude/agents/test-writer.md) |
| Agente `test-verifier` — audita de forma independiente, sin permiso de escritura | [`.claude/agents/test-verifier.md`](.claude/agents/test-verifier.md) |

Documentado también en [`CLAUDE.md`](CLAUDE.md#pruebas-unitarias), junto a la
sección análoga que ya existía para pruebas de integración.

`test-writer` nunca modifica código de producción para hacer pasar una prueba;
si algo falla, lo reporta. `test-verifier` no tiene herramientas de escritura:
solo audita y reporta hallazgos, que corrige `test-writer` después.

## 5. Cómo reproducir todo

```bash
git clone https://github.com/JesusFerru/dipl-arqui-microservicios.git
cd dipl-arqui-microservicios
git checkout feat/testing

dotnet build Nurtricenter/Nurtricenter.MS3.sln
dotnet test  Nurtricenter/Nurtricenter.MS3.Tests/Nurtricenter.MS3.Tests.csproj
```

No requiere Docker ni PostgreSQL: las pruebas unitarias no tocan infraestructura.

## Notas honestas

- Las pruebas existentes (88) están nombradas en inglés
  (`Handle_creates_contract_when_patient_and_plan_exist`). `CLAUDE.md` pide
  nombres de pruebas en español; el skill `unit-testing` y el agente
  `test-writer` ya aplican español para pruebas nuevas. No se renombraron las
  88 existentes en este avance para no generar un diff masivo sin valor
  funcional — queda como pendiente a decidir antes del Proyecto Final.
- La cobertura de `Nurtricenter.MS3.Api` e `Infrastructure` es 0% *por diseño*
  de esta capa de pruebas, no por descuido: esas capas se validan con pruebas
  de integración reales (Tarea 2), que ya existen y pasan (15/15).
