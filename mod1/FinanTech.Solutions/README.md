# Proyecto Final - Modulo 1 - Diplomado Arquitectura con Microservicios
## Luis Jesús Ferrufino Burgos - 856214 

# Tabla de Contenido
- [1. Descripción del Proyecto](#1-descripción-del-proyecto)
- [2. Estructura del Proyecto](#2-estructura-del-proyecto)
- [3. Patrones de diseño aplicados](#3-patrones-de-diseño-aplicados)
  - [3.1 Strategy](#31-strategy)
  - [3.2 Factory](#32-factory)
  - [3.3 Builder](#33-builder)
  - [3.4 Decorator](#34-decorator)
- [4. Cumplimiento de Principios SOLID](#4-cumplimiento-de-principios-solid)
- [5. Instrucciones de Ejecución](#5-instrucciones-de-ejecución)


---

## 1. Descripción del Proyecto 

Los Reportes para la empresa FinantialTech Solutions tiene las siguientes características:

0. Contenido del reporte
- Titulo
- Código encriptado del reporte (en casos encriptados)
- Fecha de generación
- Contenido
- Información detallada  (en casos detallados)
- Listado de gráficos (en casos detallados)
- Conclusiones
- Firma del responsable
- Anexos  (en casos detallados)

1. Formato:
- PDF
- Excel
- CSV
- Otros (no definidos)

2. Nivel de procesamiento:
- Básico
- Detallado
- Encriptado
- Otros (no definidos)

3. Canales de envío:
- Correo
- Almacenamiento de carpeta compartida
- Api
- Otros (no definidos)

### Funciones del sistema
1. Generar reportes a partir de datos agregados

2. Aplicar diferentes estrategias de procesamiento segn el tipo de usuario (Ejecutivo, Auditor, Analista).

3. Transformar el contenido del reporte aplicando mejoras opcionales (encabezados, marcas de agua, cifrado, compresin).

4. Entregar el reporte a través de diferentes mecanismos (cada uno debe ser intercambiable sin modificar el código de la lógica principal).

5. Soportar nuevas estrategias o tipos de entrega sin modificar el código existente.

#### Información adicional obtenida de los requisitos
1. Los tipos de usuarios son los siguientes:
- Ejecutivo: Requiere reportes de alto nivel, con información resumida y visualizaciones claras.
- Auditor: Necesita reportes detallados, con información encriptada
- Analista: Requiere reportes con un nivel de detalle, con información procesada y visualizaciones útiles para la toma de decisiones.

2. El reporte puede transformar el contenido con mejoras opcionales extras:
- Encabezado
- Marca de agua
- Cifrado
- Compresión

---

## 2. Estructura del Proyecto

El sistema se compone de dos proyectos bajo una arquitectura desacoplada:

*   **`Finantech.Solutions.Core`:** Biblioteca de clases que contiene las entidades de dominio, contratos (`interfaces`) e implementaciones concretas de los patrones de diseño. Cero dependencias de interfaces de usuario o infraestructura externa.
*   **`FinanTech.Solutions.App`:** Aplicación de consola interactiva que orquesta el flujo de negocio y actúa como cliente de la lógica del core.

```
FinanTech.Solutions/
│
├── FinanTech.Solutions.sln
│
├── Finantech.Solutions.Core/            # Lógica de Negocio y Patrones
│   ├── Builder/                        # Construcción de Formatos (PDF, Excel, CSV)
│   ├── Decorator/                      # Mejoras y Transformaciones dinámicas
│   ├── Factory/                        # Centralización en la Creación de Objetos
│   ├── Models/                         # Entidades del Dominio (Report, FinancialData)
│   └── Strategy/                       # Algoritmos de Procesamiento y Entrega
│
└── FinanTech.Solutions.App/             # Interfaz de Consola y Orquestación
    ├── Handler/                        # Manejador de Menús y Entrada de Usuario
    └── Program.cs                      # Entry Point del Sistema
```

---

## 3. Patrones de diseño aplicados

### 3.1. Strategy
El patrón **Strategy** se usa al encapsular algoritmos y hacerlos intercambiables en tiempo de ejecución. En este proyecto se aplica en 2 flujos independientes:

#### A. Estrategia de Procesamiento de Datos (`IProcessingStrategy`)
Dependiendo del rol del usuario, los datos brutos del reporte (`FinancialData`) requieren diferentes niveles de procesamiento y detalle.
*   **Contratos e Implementaciones:**
    *   `IProcessingStrategy.cs` (Interfaz base)
    *   `ExecutiveProcessingStrategy`: Genera un reporte de alto nivel resumido.
    *   `AuditorProcessingStrategy`: Genera un reporte detallado incluyendo un código de seguridad criptográfico para auditoría.
    *   `AnalystProcessingStrategy`: Genera un reporte con datos analíticos profundos, gráficos y anexos.
*   **Código clave (`IProcessingStrategy.cs`):**
    ```csharp
    public interface IProcessingStrategy
    {
        Report ProcessData(FinancialData data);
    }
    ```

#### B. Estrategia de Entrega del Reporte (`IDeliveryStrategy`)
Permite cambiar el canal físico o destino de entrega de forma dinámica.
*   **Contratos e Implementaciones:**
    *   `IDeliveryStrategy.cs` (Interfaz base)
    *   `EmailDeliveryStrategy`: Realiza el envío simulando un servidor SMTP.
    *   `SharedFolderDeliveryStrategy`: Almacena el reporte en una ruta local simulando OneDrive/Carpeta Compartida.
    *   `ApiDeliveryStrategy`: Simula la entrega a través de una petición REST API corporativa.
*   **Código clave (`IDeliveryStrategy.cs`):**
    ```csharp
    public interface IDeliveryStrategy
    {
        void Deliver(string finalReport, string destination);
    }
    ```

> [!NOTE]
> **Justificación:** Permite cumplir con el principio de Abierto/Cerrado (OCP), ya que si en el futuro la empresa desea agregar una nueva estrategia de procesamiento (ejemplo: *Operador*) o un nuevo canal de entrega (ejemplo: *Slack*), solo se debe crear la nueva clase que implemente la interfaz correspondiente, sin alterar la lógica de control del programa principal.

---

### 3.2. Factory
El patrón **Simple Factory** se utiliza para centralizar la instanciación de objetos basados en condiciones del entorno o entradas de usuario.
*   **Implementaciones:**
    *   `ProcessingStrategyFactory`: Devuelve la estrategia de procesamiento correcta (`IProcessingStrategy`) en función del rol de usuario (`UserType`).
    *   `ReportBuilderFactory`: Devuelve la instancia del constructor adecuado (`IReportBuilder`) en función del formato de salida (`PDF`, `EXCEL`, `CSV`).
    *   `DeliveryStrategyFactory`: Devuelve el canal de envío adecuado (`IDeliveryStrategy`) según la selección del canal.
*   **Código clave (`ReportBuilderFactory.cs`):**
    ```csharp
    public static class ReportBuilderFactory
    {
        public static IReportBuilder GetBuilder(string format)
        {
            return format.ToUpper() switch
            {
                "PDF" => new PdfReportBuilder(),
                "EXCEL" => new ExcelReportBuilder(),
                "CSV" => new CsvReportBuilder(),
                _ => throw new ArgumentException($"Formato '{format}' no soportado", nameof(format))
            };
        }
    }
    ```

> [!NOTE]
> **Justificación:** Desacopla al orquestador principal (`Program.cs`) de la instanciación de clases concretas (evitando el uso de `new` repetitivo y condicionales anidados). Al centralizar la lógica de instanciación, si una implementación cambia sus dependencias o constructores, solo se modifica en la fábrica respectiva.

---

### 3.3. Builder
El patrón **Builder** separa la construcción paso a paso de un objeto complejo de su representación final, permitiendo que el mismo proceso de construcción produzca estructuras con diferentes formatos.
*   **Contratos e Implementaciones:**
    *   `IReportBuilder.cs` (Contrato fluido para inicializar, aplicar formato y construir).
    *   `PdfReportBuilder`: Formatea el reporte inyectándole etiquetas y metadatos específicos del layout PDF.
    *   `ExcelReportBuilder`: Inyecta el layout de filas/celdas y metadatos de Excel.
    *   `CsvReportBuilder`: Formatea las secciones estructuradas simulando la delimitación por comas.
*   **Código clave (`IReportBuilder.cs`):**
    ```csharp
    public interface IReportBuilder
    {
        IReportBuilder Initialize(Report baseReport);
        IReportBuilder ApplyFormatLayout();
        Report Build();
    }
    ```

> [!NOTE]
> **Justificación:** Los reportes de FinanTech son objetos con una estructura rica y compleja (Título, Código encriptado, Fecha, Contenido, Gráficos, Conclusiones, Firmas, Anexos). El Builder asegura que la creación de esta estructura base sea siempre consistente e incremental, independientemente de si el formato de salida físico es PDF, Excel o CSV.

---

### 3.4. Decorator
Permite añadir responsabilidades y transformaciones al contenido del reporte de forma dinámica y combinable en tiempo de ejecución 
sin alterar el código de la clase Report original, cumpliendo con el principio Abierto/Cerrado (OCP).

Se decidió aplicar los Decorators de manera externa al procesamiento de datos (Strategies) para mantener un acoplamiento débil. 
De esta forma, las estrategias no conocen la existencia de las mejoras visuales o de cifrado, 
permitiendo modificar las reglas de transformación del documento en el pipeline principal sin alterar la lógica de negocio
de los roles de usuario.

*   **Estructura del Patrón:**
    *   `IReportComponent` (Interfaz común): Contrato que implementa tanto el reporte base como los decoradores.
    *   `ReportDecorator` (Decorador abstracto): Encapsula la llamada al componente decorado (`_wrappedReport`).
    *   Decoradores Concretos:
        *   `HeaderDecorator`: Agrega automáticamente encabezados de confidencialidad para usuarios *Ejecutivos*.
        *   `WatermarkDecorator`: Agrega marcas de agua a petición del usuario.
        *   `EncryptionDecorator`: Cifra el contenido del reporte automáticamente si proviene de una *Auditoría* (detectable si el reporte ya posee un código encriptado).
        *   `CompressionDecorator`: Aplica compresión ZIP de forma opcional.

*   **Código clave (`ReportDecorator.cs`):**
    ```csharp
    public abstract class ReportDecorator : IReportComponent
    {
        protected readonly IReportComponent _wrappedReport;

        protected ReportDecorator(IReportComponent report)
        {
            _wrappedReport = report;
        }

        public virtual string Export() => _wrappedReport.Export();
        // ... Delegación del resto de las propiedades
    }
    ```

---

## 4. Cumplimiento de Principios SOLID

El desarrollo de este sistema se guio bajo las buenas prácticas y principios de desarrollo limpio:
*   **Single Responsibility Principle (SRP):** Cada clase tiene una única responsabilidad bien definida (ej. las Fábricas instancian, las Estrategias procesan/envían, los Builders formatean y los Decoradores transforman).
*   **Open/Closed Principle (OCP):** El sistema es 100% extensible sin modificar el código existente. Si queremos agregar un nuevo decorador (ejemplo: `DigitalSignatureDecorator`), solo creamos la clase respectiva y la apilamos en el pipeline principal.
*   **Liskov Substitution Principle (LSP):** Todos los subtipos y decoradores pueden sustituir a `IReportComponent` sin romper el comportamiento esperado de la exportación del reporte.
*   **Interface Segregation Principle (ISP):** Las interfaces son pequeñas y específicas de su dominio (`IProcessingStrategy`, `IDeliveryStrategy`, `IReportBuilder`, `IReportComponent`), evitando obligar a clases concretas a depender de métodos que no utilizan.
*   **Dependency Inversion Principle (DIP):** El flujo principal en `Program.cs` interactúa única y exclusivamente con abstracciones, nunca con implementaciones concretas de estrategias o constructores.

---

## 5. Instrucciones de Ejecución

### Prerrequisitos
*   [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) o superior instalado localmente.

### Pasos para Ejecutar
1. Abra su terminal o línea de comandos favorita en el directorio raíz del proyecto (`FinanTech.Solutions`).
2. Compile y ejecute el proyecto de consola:
   ```bash
   dotnet run --project FinanTech.Solutions.App/FinanTech.Solutions.App.csproj
   ```
3. Interactúe con los menús numéricos del sistema en consola para simular la generación, formateo, decoración y envío del reporte financiero.



