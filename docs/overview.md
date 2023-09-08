# Overview

El proyecto de indicadores se divide en dos proyectos, back y front. Del lado del back se utiliza .NET en su versión 7. Del lado del front se utiliza React TS con ayuda de Vite.

## Backend

### Capas

- Domain - Dominio.
- Application - Aplicación.
- Infrastructure - Infraestructura.
- Persistence - Persistence.
- Presentation - Presentación.
- WebApi

### Patrones

- [CQRS](https://learn.microsoft.com/es-es/azure/architecture/patterns/cqrs)
- [Unit of Work](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
- [DDD](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice) - No es un patrón como tal, pero es un conjunto de patrones basados en el manejo de dominio.
- [Feature Folder Structure](https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/).

## Elementos

- [Domain](./api/domain/domain.md).
- [Application](./api/application/).
- [Infrastructure](./api/infrastructure/).
- [Persistence](./api/persistence/persistence.md).
- [Presentation](./api/presentation/presentation.md).
- [WebApi](./api/webapi/webapi.md).
