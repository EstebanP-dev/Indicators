# Domain

En la capa de dominio encontraremos todo el modelo de negocio. En este caso, entidades y repositorios que serán usados en la capa de [Persistencia](#persistence).

## Librerias

- [ErrorOr](https://github.com/amantinband/error-or)

## Conceptos

- [Strongly-typed Ids](https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/)

- [Value Objects](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects)

## Estructura de Carpetas

```shell
├───Errors
├───Exceptions
├───Features
│   ├───ActorTypes
│   ├───Displays
│   ├───IndicatorTypes
│   ├───Meanings
│   ├───Roles
│   ├───Sections
│   ├───Sources
│   └───Users
├───Primitives
├───Repositories
├───Services
└───Utils
```

## Elementos

- [Domain Errors](domainErrors.md).
- [Custom Exceptions](customExceptions.md).
- [Primitives](./primitives/pagination.md).
- [Entities](./entities/entities.md).
- [Repositories](./repositories/Irepository.md).
- [Services](./services/services.md).
