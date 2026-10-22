# 🏆 API REST para la Gestión de Reservas de Instalaciones Deportivas

Este proyecto consiste en el desarrollo de una **API REST** diseñada para gestionar las instalaciones, clientes y reservas de un complejo deportivo. Permite controlar la disponibilidad de espacios, calcular importes en base a tarifas por hora y validar solapamientos horarios.

---

## 📌 Características Principales

*   **Gestión de Instalaciones:** CRUD completo con soporte para diferentes deportes (pádel, tenis, fútbol, natación), capacidades y estados de disponibilidad.
*   **Gestión de Clientes:** Registro y administración de usuarios con validación de correo electrónico único.
*   **Control de Reservas Automatizado:**
    *   Cálculo automático de importes según la duración y el precio por hora de la instalación.
    *   Persistencia del precio cobrado de manera histórica.
    *   Validación estricta para evitar reservas simultáneas en el mismo horario e instalación.
    *   Flujo de estados de reserva (`Pendiente`, `Confirmada`, `Cancelada`, `Finalizada`).
*   **Consultas Especializadas:** Endpoints específicos para verificar disponibilidad horaria y filtrar reservas por cliente o estado.

---

## 🛠 Arquitectura y Tecnologías

El proyecto sigue una arquitectura **organizada por capas**, garantizando la separación de responsabilidades y la inyección de dependencias a través de interfaces de repositorio.

*   **Framework Principal:** .NET Core Web API (ASP.NET Core)
*   **Persistencia de Datos:** Entity Framework Core (Enfoque Code-First con Migraciones)
*   **Base de Datos:** MySQL
*   **Documentación Interactiva:** Swagger UI
*   **Transferencia de datos:** Uso de DTOs (*Data Transfer Objects*) para desacoplar las entidades de la base de datos de los contratos de la API.

---

## 🏗 Estructura del Proyecto

A continuación se detalla la organización de los archivos y componentes principales en la solución de Visual Studio:

```text
AppInstalacionesDeporitvas/
├── Controllers/                  # Controladores de la API REST
│   ├── ClientesControllers.cs
│   ├── InstalacionesDeportivasControllers.cs
│   └── ReservasControllers.cs
├── Data/                         # Contexto de Base de Datos y Datos Iniciales
│   ├── DeporteDbContext.cs       # DbContext de Entity Framework Core
│   └── DemoDataSeeder.cs         # Sembrado de datos para pruebas iniciales
├── DTOs/                         # Objetos de Transferencia de Datos (Data Transfer Objects)
├── Enums/                        # Enumerados del Sistema
│   ├── EstadoReserva.cs          # (Pendiente, Confirmada, Cancelada, Finalizada)
│   └── TipoDeporte.cs            # (Pádel, Tenis, Fútbol, Natación)
├── Migrations/                   # Historial de Migraciones de la Base de Datos
└── Models/                       # Modelos de Datos del Dominio
    └── EntidadesDeportivas.cs    # Definición de las Entidades del Negocio
```

---

## 🚀 Instalación y Configuración

### Requisitos Previos
*   [.NET SDK](https://microsoft.com) (Versión 8.0 o superior recomendada)
*   [MySQL Server](https://mysql.com) instalado y en ejecución

### Pasos para el despliegue

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com
   cd AppInstalacionesDeporitvas
   ```

2. **Configurar la cadena de conexión:**
   Abre el archivo `appsettings.json` en la raíz del proyecto y añade tus credenciales de MySQL:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=DeporteDb;Uid=TU_USUARIO;Pwd=TU_CONTRASEÑA;"
   }
   ```

3. **Ejecutar las migraciones de Entity Framework:**
   Genera la base de datos y las tablas ejecutando el siguiente comando en la terminal:
   ```bash
   dotnet ef database update
   ```

4. **Iniciar la aplicación:**
   ```bash
   dotnet run
   ```

---

## 💻 Pruebas con Swagger

Una vez iniciada la aplicación, la documentación interactiva e interfaz de pruebas estará disponible en la siguiente dirección local:
👉 `https://localhost:XXXX/swagger/index.html` *(Reemplazar `XXXX` por el puerto SSL asignado por tu entorno)*.

Desde Swagger se pueden probar los flujos clave requeridos:
*   Creación exitosa de reservas con cálculo automático de precios.
*   Consulta dinámica de disponibilidad por rango de fechas y horas.
*   Validación de rechazo de reservas duplicadas o solapadas.
