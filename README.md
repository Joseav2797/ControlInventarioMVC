📦 Control de Inventario MVC

  

Sistema de control de inventario desarrollado con ASP.NET Core MVC y SQL Server. Permite la gestión de artículos, movimientos de inventario y ubicaciones de manera eficiente.

🚀 Características

🏢 Gestión de Artículos: Agregar, editar y eliminar artículos del inventario.

📦 Movimientos de Inventario: Registrar entradas y salidas de productos.

📍 Ubicaciones: Controlar en qué parte del almacén se encuentran los productos.

🔐 Autenticación JWT: Seguridad para acceso a la API.

📊 Logging con Serilog: Registro de eventos para monitoreo.

🛠️ Tecnologías Usadas

.NET Core 8.0

Entity Framework Core

SQL Server

Serilog

Bootstrap 5

Heroku (para despliegue)

📥 Instalación y Configuración

🔧 Requisitos

.NET SDK 8.0

SQL Server (o Azure SQL)

Visual Studio / VS Code

🔌 Configuración


Configurar la cadena de conexión en appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=ControlInventarioDB;User Id=sa;Password=TU_CONTRASEÑA;TrustServerCertificate=True;"
}

Restaurar dependencias e iniciar la aplicación:

dotnet restore
dotnet run

Puedes descargar la base de datos desde este enlace:
🔗 https://drive.google.com/file/d/19pMgNv-qpy-7xmonVw18U2vmzSeI3r0N/view?usp=sharing

🛠️ Contribuciones

Las contribuciones son bienvenidas. Para colaborar:

Haz un fork del repositorio.

Crea una rama con tu feature: git checkout -b feature/nueva-funcion.

Haz commit de tus cambios: git commit -m 'Agregada nueva función X'.

Haz push a la rama: git push origin feature/nueva-funcion.

Abre un pull request 🚀.

📌 Autor: Juan Jose Alvarez Valencia 🧑‍💻
