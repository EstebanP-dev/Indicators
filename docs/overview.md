# Overview

El proyecto de indicadores se divide en dos proyectos, back y front. Del lado del back se utiliza .NET en su versión 7. Del lado del front se utiliza React TS con ayuda de Vite. En ambos proyectos se implementan las architecturas limpias a diferencia que en el web es con [Arquitectura Hexagonal](https://dev.to/juanoa/folder-structure-in-a-react-hexagonal-architecture-h77).

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

## Elementos de la Solución

- [Domain](./api/domain/domain.md).
- [Application](./api/application/).
- [Infrastructure](./api/infrastructure/).
- [Persistence](./api/persistence/persistence.md).
- [Presentation](./api/presentation/presentation.md).
- [WebApi](./api/webapi/webapi.md).

## Frontend

El proyecto como anteriormente se menciona se realiza en con React TS. Para crear el proyecto es necesario usar Vite.js e implementarlo con typescript. Adicional utilizo el manejador de paquetes [Yarn](https://yarnpkg.com/).

Para los estilos, se implementó la extesión de css [Sass](https://sass-lang.com/).

## Librerias adicionales

- [Sass](https://sass-lang.com/).
- [Redux](https://redux.js.org/).
- [Axios](https://axios-http.com/docs/intro).
- [Mui X](https://mui.com/x/introduction/).

## Elementos del Source

- [Components](./web/components/).
- [Enviroments](./web/enviroments/enviroments.md).
- [Hooks](./web/hooks/hooks.md).
- [Models](./web/models/models.md).
- [Redux](./web/redux/redux.md).
- [Services](./web/services/services.md).
- [Utilities](./web/utilities/utilities.md).
