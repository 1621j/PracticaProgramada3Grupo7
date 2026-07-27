# Práctica Programada 3 - Sistema de Votación Nacional

Proyecto desarrollado para el curso Programación Avanzada de la carrera Ingeniería en Sistemas de Computación.

## Integrantes

* Heiner Calderón Montero
* Jessica Porras Canales
* Alex Felipe Bolaños Alfaro
* Kendal Salas Gonzalez

## Repositorio

[PracticaProgramada3Grupo7](https://github.com/1621j/PracticaProgramada3Grupo7)

## Descripción

El proyecto consiste en un sistema de votación nacional que permite:

* Agregar, consultar, actualizar y eliminar votantes.
* Agregar, consultar, actualizar y eliminar partidos políticos.
* Validar el ingreso del votante mediante su número de cédula.
* Comprobar que el votante esté inscrito y activo.
* Registrar el voto por un partido político.
* Impedir que una persona vote más de una vez.
* Consultar los resultados electorales.
* Mostrar la cantidad de votos y porcentaje obtenido por cada partido.

## Arquitectura

La solución utiliza una arquitectura por capas compuesta por cuatro proyectos:

### PracticaProgramada3Grupo7

Proyecto ASP.NET Core MVC encargado de la interfaz gráfica y de consumir la API REST.

### PracticaProgramada3Grupo7.API

Proyecto ASP.NET Core Web API encargado de recibir las solicitudes HTTP y comunicarse con la capa de lógica de negocio.

### PracticaProgramada3Grupo7.BLL

Capa de lógica de negocio. Contiene:

* DTO.
* Servicios.
* Validaciones.
* Reglas del proceso de votación.
* Mapeo manual entre entidades y DTO.

### PracticaProgramada3Grupo7.DAL

Capa de acceso a datos. Contiene:

* Entidades.
* Repositorios.
* `ApplicationDbContext`.
* Migraciones de Entity Framework Core.
* Comunicación con SQL Server.

## Flujo de la aplicación

```text
MVC → API REST → BLL → DAL → SQL Server
```

El proyecto MVC no se conecta directamente con la base de datos. Toda la información se obtiene mediante solicitudes HTTP a la API REST.

## Base de datos

Motor utilizado:

```text
SQL Server
```

Nombre de la base:

```text
PracticaProgramada3Grupo7Db
```

Tablas principales:

* `Votantes`
* `PartidosPoliticos`
* `Votos`
* `__EFMigrationsHistory`

La base de datos incluye restricciones para evitar:

* Cédulas duplicadas.
* Siglas de partidos duplicadas.
* Más de un voto por votante.
* Eliminación de votantes o partidos que tengan votos relacionados.

## Paquetes NuGet

Paquetes utilizados:

* `Microsoft.EntityFrameworkCore`
* `Microsoft.EntityFrameworkCore.SqlServer`
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.EntityFrameworkCore.Design`
* `Swashbuckle.AspNetCore`

Las versiones deben coincidir con la versión de .NET utilizada por la solución.

No se utiliza AutoMapper. El mapeo se realiza manualmente mediante la clase:

```text
MapeoClases.cs
```

## Principios y prácticas utilizadas

### Responsabilidad única

Cada clase tiene una responsabilidad específica:

* Los controladores reciben las solicitudes.
* Los servicios aplican las reglas de negocio.
* Los repositorios se encargan del acceso a datos.
* Los DTO transportan la información entre capas.
* Las entidades representan las tablas de la base de datos.

### Separación de responsabilidades

La interfaz, la API, la lógica de negocio y el acceso a datos se encuentran separados en distintos proyectos.

### Inyección de dependencias

Los repositorios, servicios y el contexto de Entity Framework se registran en `Program.cs` y se reciben mediante los constructores.

### Programación asíncrona

Las operaciones de base de datos utilizan `async` y `await` para evitar bloquear la ejecución de la aplicación.

## Patrones utilizados

### Repository

Los repositorios encapsulan las consultas y operaciones realizadas con Entity Framework Core.

### Service Layer

Los servicios contienen las validaciones y reglas de negocio del sistema.

### DTO

Los DTO permiten trasladar información sin exponer directamente las entidades de la base de datos.

### MVC

El frontend utiliza el patrón Modelo-Vista-Controlador.

### Dependency Injection

Las dependencias se administran mediante el contenedor integrado de ASP.NET Core.

## API REST

### Votantes

```text
GET    /api/Votantes
GET    /api/Votantes/{id}
GET    /api/Votantes/cedula/{cedula}
POST   /api/Votantes
PUT    /api/Votantes/{id}
DELETE /api/Votantes/{id}
```

### Partidos políticos

```text
GET    /api/PartidosPoliticos
GET    /api/PartidosPoliticos/activos
GET    /api/PartidosPoliticos/{id}
POST   /api/PartidosPoliticos
PUT    /api/PartidosPoliticos/{id}
DELETE /api/PartidosPoliticos/{id}
```

### Votación

```text
POST /api/Votacion
GET  /api/Votacion/resultados
```

## Funcionalidades probadas

* Registro de votantes.
* Consulta de votantes.
* Búsqueda por cédula.
* Actualización de votantes.
* Eliminación de votantes sin votos.
* Rechazo de cédulas duplicadas.
* Registro de partidos políticos.
* Consulta y actualización de partidos.
* Eliminación de partidos sin votos.
* Rechazo de siglas duplicadas.
* Registro de votos.
* Rechazo de cédulas no inscritas.
* Rechazo de votantes inactivos.
* Prevención del voto doble.
* Consulta de resultados y porcentajes.
* Restricción de eliminación para registros con votos.

## Configuración

La cadena de conexión se encuentra en:

```text
PracticaProgramada3Grupo7.API/appsettings.json
```

Ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PracticaProgramada3Grupo7Db;Integrated Security=True;TrustServerCertificate=True;"
}
```

Cada integrante debe ajustar el servidor según su instalación local de SQL Server.

## Crear o actualizar la base de datos

Desde la consola de administración de paquetes:

```powershell
Update-Database -Project PracticaProgramada3Grupo7.DAL -StartupProject PracticaProgramada3Grupo7.API
```

## Ejecución

Para utilizar el sistema completo deben ejecutarse simultáneamente:

1. `PracticaProgramada3Grupo7.API`
2. `PracticaProgramada3Grupo7`

La API debe iniciarse antes o al mismo tiempo que el proyecto MVC.

## Estado del proyecto

### Completado

* Arquitectura inicial.
* Entidades.
* DbContext.
* Migración inicial.
* Base de datos SQL Server.
* Repositorios.
* DTO.
* Mapeo manual.
* Servicios de negocio.
* API REST.
* Validaciones principales.
* Pruebas de endpoints en Swagger.

### Pendiente

* ViewModels del proyecto MVC.
* Configuración de `HttpClient`.
* Servicios para consumir la API.
* Controladores MVC.
* Vistas de votantes.
* Vistas de partidos políticos.
* Pantalla para ingresar la cédula.
* Pantalla para seleccionar el partido.
* Confirmación del voto.
* Pantalla de resultados.
* Navegación y diseño visual.
* Pruebas integradas entre MVC y API.
* Revisión final de nombres, estilos y validaciones.
* Archivo ZIP final para entregar.
