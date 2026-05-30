# Proyecto Final - Modulo 1 - Diplomado Arquitectura con Microservicios
## Luis Jesús Ferrufino Burgos - 856214 

# Tabla de Contenido
- [Descripción del Proyecto](#1-descripción-del-proyecto)
- [Patrones de diseño aplicados](#2-patrones-de-diseño-aplicados)
- 

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

2. Aplicar diferentes estrategias de procesamiento según el tipo de usuario (Ejecutivo, Auditor, Analista).

3. Transformar el contenido del reporte aplicando mejoras opcionales (encabezados, marcas de agua, cifrado, compresión).

4. Entregar el reporte a través de diferentes mecanismos (cada uno debe ser intercambiable sin modificar el código de la lógica principal).

5. Soportar nuevas estrategias o tipos de entrega sin modificar el código existente.

#### Información adicional obtenida de los requisitos
1. Los tipos de usuarios son los sgtes:
- Ejecutivo: Requiere reportes de alto nivel, con información resumida y visualizaciones claras.
- Auditor: Necesita reportes detallados, con información encriptada
- Analista: Requiere reportes con un nivel de detalle, con información procesada y visualizaciones útiles para la toma de decisiones.

2. El reporte puede transformar el contenido con mejoras opcionales extras:
- Encabezado
- Marca de agua
- Cifrado
- Compresión

## 2. Patrones de diseño aplicados

### 1. Strategy


### 2. Factory

### 3. Builder

### 4. Decorator
Permite añadir responsabilidades y transformaciones al contenido del reporte de forma dinámica y combinable en tiempo de ejecución 
sin alterar el código de la clase Report original, cumpliendo con el principio Abierto/Cerrado (OCP).

Se decidió aplicar los Decorators de manera externa al procesamiento de datos (Strategies) para mantener un acoplamiento débil. 
De esta forma, las estrategias no conocen la existencia de las mejoras visuales o de cifrado, 
permitiendo modificar las reglas de transformación del documento en el pipeline principal sin alterar la lógica de negocio
de los roles de usuario.


