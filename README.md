# N5 .NET 8 Web API Challenge

## Descripción
Este proyecto consiste en desarrollar una **Web API** en **.NET 8** para gestionar el registro de permisos de usuarios. La aplicación debe seguir las mejores prácticas de desarrollo, incluyendo el uso de **Entity Framework**, **Elasticsearch**, **patrón de repositorio y unidad de trabajo**, y **CQRS**. Además, se debe construir una aplicación **ReactJS** con **Axios** para consumir la API y desarrollar pruebas unitarias.

## Tecnologías Utilizadas
- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core** (EF Core)
- **SQL Server**
- **Elasticsearch**
- **CQRS**
- **ReactJS**
- **Axios**
- **xUnit** (para pruebas unitarias)

## Requisitos
1. **API en .NET 8**
   - CRUD de permisos de usuario.
   - Implementación del patrón **Repositorio y Unidad de Trabajo**.
   - Uso de **Entity Framework Core** con **SQL Server**.
   - Integración con **Elasticsearch**.
   - Implementación de **CQRS**.
   - Pruebas unitarias.

2. **Aplicación en ReactJS**
   - Consumo de la API con **Axios**.
   - Interfaz de usuario simple para gestionar los permisos.

## Instalación y Configuración
### 1. Clonar el repositorio
```sh
git clone <repo_url>
cd <nombre_proyecto>
```

### 2. Configurar la Base de Datos
- Asegurarse de tener **SQL Server** en ejecución.
- Crear la base de datos `N5`.
- Configurar la cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DANTE;Database=N5;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

### 3. Aplicar Migraciones de EF Core
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Ejecutar la API
```sh
dotnet run
```

### 5. Ejecutar la Aplicación React
```sh
cd client-app
npm install
npm start
```

## Endpoints Principales
### **1. Crear un Permiso**
```http
POST /api/permissions
```
**Body:**
```json
{
  "userId": 1,
  "permissionType": "Read"
}
```

### **2. Obtener Permisos**
```http
GET /api/permissions
```

### **3. Modificar un Permiso**
```http
PUT /api/permissions/{id}
```
**Body:**
```json
{
  "permissionType": "Write"
}
```

### **4. Eliminar un Permiso**
```http
DELETE /api/permissions/{id}
```

## Pruebas Unitarias
Para ejecutar las pruebas:
```sh
dotnet test
```

## Autores
- **[Tu Nombre]**

