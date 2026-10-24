# Tarea 2 — Integration Testing (Nurtricenter MS3)

Certifica el avance de la Actividad 2 (taller de Integration Tests) sobre el
caso de estudio del proyecto final, aplicado al microservicio **Nurtricenter
MS3**.

**Repositorio:** https://github.com/JesusFerru/dipl-arqui-microservicios
**Rama:** `feat/testing-2`
**Herramienta:** xUnit + `WebApplicationFactory` desde Visual Studio / el IDE
del proyecto (alternativa explícita a Postman que permite el enunciado: "Como
herramientas utilizar Postman o Visual Studio o su IDE de preferencia donde
estén desarrollando el proyecto"). Se usó el IDE porque el microservicio ya se
desarrolla en .NET — mantiene las pruebas versionadas junto al código, tipadas
contra los DTOs reales, y ejecutables con el mismo `dotnet test` que corre el
resto de la suite.

## Qué pedía la actividad

1. Aplicar el taller de Integration Tests al caso de estudio — avance, no
   necesariamente completo.
2. Al menos un flujo correcto y uno incorrecto para considerarse presentada.
3. Crear un `integration-test-writer` con sus skills y un entorno validado de
   pruebas de integración para IA.
4. Presentar un enlace al repositorio.

## 1. Integration tests aplicados

Suite en [`Nurtricenter/Nurtricenter.MS3.IntegrationTests/`](Nurtricenter/Nurtricenter.MS3.IntegrationTests/),
contra la API real levantada en memoria (`WebApplicationFactory`) con SQLite
in-memory en lugar de PostgreSQL — no requiere Docker ni base de datos para
correr.

| Archivo | Qué cubre |
|---|---|
| [`ContractLifecycleTests.cs`](Nurtricenter/Nurtricenter.MS3.IntegrationTests/ContractLifecycleTests.cs) | **Flujo correcto** — ciclo de vida completo del contrato: crear → consultar → cobrar → verificar estado y evento saliente `CreateSchedule` (MS4) → consultar factura → cancelar → consultar estado final. 1 prueba, 8 pasos encadenados. |
| [`PackageLifecycleTests.cs`](Nurtricenter/Nurtricenter.MS3.IntegrationTests/PackageLifecycleTests.cs) | **Flujo correcto** — ciclo de vida completo del paquete: crear → ensamblar → etiquetar → validar → verificar evento saliente `TransferToLogistics` (MS5). 3 pruebas (la principal + 2 de regresión, ver sección 3). |
| [`ContractErrorFlowTests.cs`](Nurtricenter/Nurtricenter.MS3.IntegrationTests/ContractErrorFlowTests.cs) | **Flujo incorrecto** — 12 escenarios de error: 404 (paciente/plan/contrato/factura/paquete/orden inexistente), 409 (copago con contrato activo), 500 (pago duplicado, cancelar sin pagar), 400 (batch no confirmado, conteo inconsistente). |
| [`HealthCheckTests.cs`](Nurtricenter/Nurtricenter.MS3.IntegrationTests/HealthCheckTests.cs) | Smoke tests — arranque de la API y endpoint de simulación de pacientes. |

```bash
dotnet test Nurtricenter/Nurtricenter.MS3.IntegrationTests/Nurtricenter.MS3.IntegrationTests.csproj
```

```
Passed!  - Failed: 0, Passed: 18, Skipped: 0, Total: 18
```

Cada paso se verifica contra el **estado persistido** (`QueryDbAsync` sobre la
base real de la prueba), no solo el código HTTP — es la regla explícita del
skill `integration-testing` y la razón por la que estas pruebas prueban algo
más que "el endpoint respondió".

## 2. Flujo correcto y flujo incorrecto (mínimo exigido)

El enunciado pide al menos uno de cada uno; el proyecto tiene dos correctos y
doce incorrectos:

- **Correcto:** contratación completa (`ContractLifecycleTests`) y producción
  completa (`PackageLifecycleTests`) — dos subdominios distintos del
  microservicio, cada uno de punta a punta.
- **Incorrecto:** los 12 casos de `ContractErrorFlowTests`, cubriendo los
  cuatro códigos de error que el sistema realmente devuelve (400/404/409/500).

## 3. Hallazgo de producción descubierto al escribir el flujo del paquete

Al escribir el flujo correcto de `PackageLifecycleTests`, el proceso de
verificación (`integration-test-verifier`) encontró que `GenerateLabelCommandHandler`
resuelve la dirección de entrega de forma incompleta:

- **Ignora el plan del paquete.** Busca en los calendarios simulados solo por
  `PatientId` (`GenerateLabelCommandHandler.cs:41`), sin cruzar
  `CatalogPlanId`. Un paciente con más de un plan activo puede recibir en su
  etiqueta la dirección del primer plan que aparezca en la simulación, no la
  del plan real del paquete.
- **No falla si no hay calendario.** Si el paciente no tiene ninguna entrada
  activa, el handler no devuelve error: usa el texto literal
  `"Address not found"` como dirección y de todas formas marca el paquete como
  `Labeled` (204).

No se tocó código de producción para "arreglar" esto — es una regla dura del
entorno de pruebas (`integration-test-writer` no modifica el sistema para que
una prueba pase). En cambio, se agregaron dos pruebas que **documentan el
comportamiento actual como pin test de regresión**:
`POST_packages_label_usa_la_direccion_del_primer_plan_del_paciente_ignorando_el_plan_del_paquete`
y `POST_packages_label_usa_placeholder_cuando_el_paciente_no_tiene_calendario_activo`.
Si en algún momento se corrige el handler, estas dos pruebas van a fallar — y
eso es exactamente lo que deben hacer.

**Queda pendiente de decisión** si corresponde corregir `GenerateLabelCommandHandler`
para que también filtre por `CatalogPlanId` y/o falle explícitamente sin
calendario. No se tomó esa decisión unilateralmente porque cambia una regla de
negocio real, no solo una prueba.

## 4. Entorno validado de pruebas de integración para IA

Ya existía desde el avance anterior — se reutilizó y se ejercitó en vivo con
el trabajo de la sección 3:

| Pieza | Archivo |
|---|---|
| Skill — contrato del entorno (`ApiFactory`, SQLite in-memory, restricciones conocidas) | [`.claude/skills/integration-testing/SKILL.md`](.claude/skills/integration-testing/SKILL.md) |
| Skill — flujos de negocio con rutas, payloads e identificadores deterministas | [`.claude/skills/test-flows/SKILL.md`](.claude/skills/test-flows/SKILL.md) |
| Agente `integration-test-writer` — escribe y ejecuta pruebas | [`.claude/agents/integration-test-writer.md`](.claude/agents/integration-test-writer.md) |
| Agente `integration-test-verifier` — audita de forma independiente, sin permiso de escritura | [`.claude/agents/integration-test-verifier.md`](.claude/agents/integration-test-verifier.md) |

El ciclo completo escritor → verificador → corrección → re-verificación se
corrió de verdad para `PackageLifecycleTests.cs` (no es solo documentación
teórica del proceso):

1. `integration-test-writer` escribió el flujo correcto del paquete.
2. `integration-test-verifier` lo auditó de forma independiente y encontró un
   fallo real: la aserción de dirección no discriminaba comportamiento
   correcto de incorrecto (pasaba "por casualidad" con los datos elegidos).
3. `integration-test-writer` corrigió agregando los dos pin tests de la
   sección 3, sin tocar producción.
4. `integration-test-verifier` re-verificó una vez (pasada acotada, no un
   loop) y confirmó el fallo resuelto.

## 5. Cómo reproducir todo

```bash
git clone https://github.com/JesusFerru/dipl-arqui-microservicios.git
cd dipl-arqui-microservicios
git checkout feat/testing-2

dotnet build Nurtricenter/Nurtricenter.MS3.sln
dotnet test  Nurtricenter/Nurtricenter.MS3.IntegrationTests/Nurtricenter.MS3.IntegrationTests.csproj
```

No requiere Docker ni PostgreSQL — `ApiFactory` sustituye el proveedor por
SQLite in-memory.

## Estado combinado de la suite (unitarias + integración)

```bash
dotnet test Nurtricenter/Nurtricenter.MS3.sln
```

```
Nurtricenter.MS3.Tests.dll:            88 passed, 0 failed   (Tarea 1)
Nurtricenter.MS3.IntegrationTests.dll: 18 passed, 0 failed   (Tarea 2)
Total: 106 passed, 0 failed
```

## Notas

- La aserción `NotBe(...)` en `POST_packages_label_usa_la_direccion_del_primer_plan_del_paciente_ignorando_el_plan_del_paquete`
  es lógicamente redundante frente al `Be(...)` que la precede (si `Be(A)`
  pasa y `A != B`, `NotBe(B)` no puede fallar independientemente). No afecta la
  validez de la prueba — el `Be` es el que realmente pinea el bug — se deja
  igual por legibilidad de la intención.
- La columna `OutgoingIntegrationEvents.Payload` está mapeada como `jsonb`
  (específico de PostgreSQL). Bajo SQLite in-memory no se traduce a un tipo
  con afinidad `TEXT`, pero no rompe nada gracias al *manifest typing* de
  SQLite; es la misma clase de desalineación esquema-de-prueba vs.
  esquema-real que el skill `integration-testing` ya advierte en su sección
  "Restricciones conocidas".
- Los endpoints de simulación de MS2 (`/sim/catalog/plans/*`) y MS4
  (`/sim/calendars/active-tomorrow`) no tienen una prueba dedicada que los
  ejercite de forma aislada — se ejercitan indirectamente a través de los
  flujos de contrato y paquete. No es un requisito de la actividad, queda
  como posible extensión.
