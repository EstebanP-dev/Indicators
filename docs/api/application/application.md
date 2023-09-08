# Application

En la capa de aplicación encontraremos toda la lógica que resuelve los casos de uso. En este caso, obtenemos los datos de la base de datos o realizamos las validaciones necesarias para hacer la petición a la db.

## Librerias

- [Mapster](https://www.bing.com/search?pglt=41&q=mapster&cvid=10604c91f9364411b1f7261ed978fc4a&aqs=edge..69i57j69i59l4j69i60l3.728j0j1&FORM=ANNTA1&PC=U531).
- [MediatR](https://github.com/jbogard/MediatR).
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/).
- [Npgsql EntityFramework Core PostgreSQL](https://github.com/npgsql/efcore.pg)
- [Microsoft Extensions Configuration](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration).

## Conceptos

- [CQRS](https://learn.microsoft.com/es-es/azure/architecture/patterns/cqrs).

## Estructura de Carpetas

```shell
├───Abstraction
│   ├───Data
│   └───Messaging
├───Features
│   ├───ActorTypes
│   │   ├───CreateActorType
│   │   ├───DeleteActorType
│   │   ├───GetActorTypeById
│   │   ├───GetActorTypesPagination
│   │   └───UpdateActorType
│   ├───Auth
│   │   └───Login
│   ├───Displays
│   │   ├───CreateDisplay
│   │   ├───DeleteDisplay
│   │   ├───GetDisplayById
│   │   ├───GetDisplaysPagination
│   │   └───UpdateDisplay
│   ├───IndicatorTypes
│   │   ├───CreateIndicatorTypes
│   │   ├───DeleteIndicatorTypes
│   │   ├───GetIndicatorTypesById
│   │   ├───GetIndicatorTypesPagination
│   │   └───UpdateIndicatorTypes
│   ├───Meanings
│   │   ├───CreateMeaning
│   │   ├───DeleteMeaning
│   │   ├───GetMeaningById
│   │   ├───GetMeaningsPagination
│   │   └───UpdateMeaning
│   ├───Roles
│   │   ├───CreateRole
│   │   ├───DeleteRole
│   │   ├───GetRoleById
│   │   ├───GetRolesPagination
│   │   └───UpdateRole
│   ├───Sections
│   │   ├───GetSectionById
│   │   ├───GetSectionsPagination
│   │   ├───GetSubSectionById
│   │   └───GetSubSectionsPagination
│   ├───Sources
│   │   ├───CreateSource
│   │   ├───DeleteSource
│   │   ├───GetSourceById
│   │   ├───GetSourcesPagination
│   │   └───UpdateSource
│   └───Users
│       ├───CreateUser
│       ├───DeleteUser
│       ├───GetUserById
│       └───GetUsersPagination
└───Validations
```

## Elementos

- [Abstractions](./abstractions/abstractions.md).
- [Features](./features/features.md).
- [Validations](./validations/validations.md).
- [Assembly](assembly.md).
