# Sistema de Gestión de Clientes

## Descripción
Sistema de gestión de clientes desarrollado en **C# con Windows Forms y Entity Framework**, que permite realizar operaciones **CRUD** (Crear, Leer, Actualizar, Eliminar) sobre la tabla `Customers` de la base de datos **Northwind**.

## Tecnologías
- C# (.NET Framework 4.7.2)
- Windows Forms
- Entity Framework 6.5.4
- SQL Server (Northwind)
- MSTest (Pruebas unitarias)

## Estructura
- **ClienteNegocio**: Lógica de negocio y DTOs
- **ClientesDatos**: Acceso a datos (Repository y DbContext)
- **ClientesCostumers**: Interfaz de usuario (Windows Forms)
- **PruebasCliente**: Pruebas unitarias (10 casos de prueba)

## Configuración

### Base de datos
El proyecto utiliza la base de datos **Northwind** y la tabla **Customers**.

### Cadena de conexión
En `App.config`, actualizar `data source` con el nombre de tu servidor SQL:

### Justificación
- Para modificar cliente se selecciona la flecha del lateral de la tabla de datagridview

```xml
<add name="NorthwindEntities" connectionString="data source=TU_SERVIDOR;initial catalog=Northwind;integrated security=SSPI;"providerName="System.Data.SqlClient" />
