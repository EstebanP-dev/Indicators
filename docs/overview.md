# Overview

El proyecto de indicadores se divide en dos proyectos, back y front. Del lado del back se utiliza .NET en su versión 7. Del lado del front se utiliza React TS con ayuda de Vite.

## Backend

### Capas

- [Domain](#domain) - Dominio.
- [Application](#application) - Aplicación.
- [Infrastructure](#infrastructure) - Infraestructura.
- [Persistence](#persistence) - Persistence.
- [Presentation](#presentation) - Presentación.
- [WebApi](#webapi).

### Patrones

- [CQRS](https://learn.microsoft.com/es-es/azure/architecture/patterns/cqrs)
- [Unit of Work](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
- [DDD](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice) - No es un patrón como tal, pero es un conjunto de patrones basados en el manejo de dominio.
- [Feature Folder Structure](https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/)

### Domain

En la capa de dominio encontraremos todo el modelo de negocio. En este caso, entidades y repositorios que serán usados en la capa de [Persistencia](#persistence).

#### Librerias

- [ErrorOr](https://github.com/amantinband/error-or)

#### Conceptos

- [Strongly-typed Ids](https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/)

- [Value Objects](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects)

#### Estructura de Carpetas

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

#### Domain Errors

Es una clase estatica con propiedades, tambien estaticas, que retornan la clase **Error** de la libreria **[ErrorOr](https://github.com/amantinband/error-or)**. El sentido de estas es tener unos textos estandarizados para cada posible error que se presente en nuestro código. Estos errores únicamente deben implementarse en la capa de [Aplicación](#application).

```csharp
/// <summary>
/// Domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Gets the undefined error.
    /// </summary>
    /// <value>
    /// The undefined error.
    /// </value>
    public static Error UndefinedError => Error.Unexpected(
            description: "An error occurred while processing the request. Try to contact the support team.");
    
    /// <summary>
    /// Gets the creation or updation failed error.
    /// </summary>
    /// <value>
    /// The creation or updation failed error.
    /// </value>
    public static Error CreationOrUpdatingFailed => Error.Failure(
        description: "Something was wrong. Try again later.");
}
```

#### Custom Exceptions

Las excepciones personalizadas, permiten tener una mejor trasabilidad e información con respecto a cualquier error que suceda en nuestra aplicación. Estas excepciones son usadas para ampliar la información de nuestro código y evitar los [Magic Strings](https://methodpoet.com/magic-strings/).

**Bad**

```csharp
throw new ArgumentException("Please supply at least one argument.");
```

**Good**

```csharp
class NeedsLeatsOneArgument : Exception
{
    public NeedsLeatsOneArgument(Exception? innerException = null)
        : base("Please supply at least one argument.", innerException)
    {}
}
```

Actualmente en el proyecto, no se han implementado los custom exceptions debido a que no ha sido necesario. En el transcurso del desarrollo se implementaran.

#### Primitives

Los objeetos primitivos, son clases que tienen un funcionamiento abstracto, es decir, buscan que otras clases hereden sus comportamientos.

##### Entity{T}

Es la clase base de las entidades que le asigna unos comportamientos especificos de la interfaz [IEquatable](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=net-7.0).

```csharp
/// <summary>
/// Entity base class.
/// </summary>
/// <typeparam name="TEntityId">Id type.</typeparam>
public abstract class Entity<TEntityId>
    : IEquatable<Entity<TEntityId>>
    where TEntityId : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    /// <param name="id">Instance of entity id.</param>
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the entity id.
    /// </summary>
    /// <value>
    /// The entity id.
    /// </value>
    public TEntityId Id { get; private set; }

    /// <summary>
    /// Gets if the entities ids are equals.
    /// </summary>
    /// <param name="left">Left entity id.</param>
    /// <param name="right">Right entity id.</param>
    /// <returns>Return if the left is equals to the right entity id.</returns>
    public static bool operator ==(Entity<TEntityId>? left, Entity<TEntityId>? right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    /// <summary>
    /// Gets if the entities ids are not equals.
    /// </summary>
    /// <param name="left">Left entity id.</param>
    /// <param name="right">Right entity id.</param>
    /// <returns>Return if the left is not equals to the right entity id.</returns>
    public static bool operator !=(Entity<TEntityId>? left, Entity<TEntityId>? right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public bool Equals(Entity<TEntityId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() == GetType())
        {
            return false;
        }

        if (obj is not Entity<TEntityId> entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}
```

##### ValueObject

Esta clase al igual que [Entity{T}](#entityt), hereda de la interfaz [IEquatable](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=net-7.0); se diferencian en que [Entity{T}](#entityt) hace las comparaciones con una subclase que equivale al id de la entidad.

Para este [ValueObject](#valueobject), creamos una interfaz en la misma carpeta **Primitives**.

```csharp
/// <summary>
/// <see cref="IEquatable{T}"/> abtract methods.
/// </summary>
/// <typeparam name="TClass"><see cref="IValueObject{T}"/> implement class.</typeparam>
public interface IValueObject<TClass> : IEquatable<TClass>
    where TClass : IValueObject<TClass>
{
}
```

```csharp
/// <summary>
/// <see cref="IValueObject{T}"/> implementation.
/// </summary>
public abstract class ValueObject : IValueObject<ValueObject>
{
    /// <summary>
    /// Equals algorithm comparative.
    /// </summary>
    /// <param name="left">Left value.</param>
    /// <param name="right">Right value.</param>
    /// <returns>Returns if the values are the same.</returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    /// <summary>
    /// Not equals algorithm comparative.
    /// </summary>
    /// <param name="left">Left value.</param>
    /// <param name="right">Right value.</param>
    /// <returns>Returns if the values are the diferents.</returns>
    public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

    /// <inheritdoc/>
    public bool Equals(ValueObject? other) => other is not null && GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        if (obj is not ValueObject valueObject)
        {
            return false;
        }

        return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (object obj in GetAtomicValues())
        {
            hashCode.Add(obj);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Gets the atomic values.
    /// </summary>
    /// <returns>Return an <see cref="IEnumerable{T}"/> of values.</returns>
    protected abstract IEnumerable<object> GetAtomicValues();
}
```

##### Pagination

La clase [Pagination](#pagination) mapea la respuesta que le daremos a usuario cuando solicite un listado paginado de datos.

```csharp
/// <summary>
/// Pagination response object.
/// </summary>
/// <typeparam name="TResponse">Entity or response model.</typeparam>
public class Pagination<TResponse>
    where TResponse : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pagination{TResponse}"/> class.
    /// </summary>
    /// <param name="totalPages">Total pages.</param>
    /// <param name="currentPage">Current page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="response">Result.</param>
    public Pagination(int totalPages, int currentPage, int pageSize, IEnumerable<TResponse> response)
    {
        TotalPages = totalPages;
        CurrentPage = currentPage;
        PageSize = pageSize;
        Response = response ?? Enumerable.Empty<TResponse>();
    }

    /// <summary>
    /// Gets total pages.
    /// </summary>
    /// <value>
    /// Total pages.
    /// </value>
    public int TotalPages { get; init; }

    /// <summary>
    /// Gets current page.
    /// </summary>
    /// <value>
    /// Current page.
    /// </value>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Gets page size.
    /// </summary>
    /// <value>
    /// Page size.
    /// </value>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets the response list.
    /// </summary>
    /// <value>
    /// The response list.
    /// </value>
    public IEnumerable<TResponse> Response { get; init; }
}
```

##### PatinationQueryParameters

Es una clase que contiene los parametros ncesarios para paginar los datos. Esta información es dada por el usuario en la petición al API.

```csharp

/// <summary>
/// Pagination parameters.
/// </summary>
public sealed record class PaginationQueryParameters(int Page, int Rows, string? Exclude);
```

**NOTA:**

- Las clases [record](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record) crean internamente los métodos get y set de las propiedades especificadas y se instancian en el constructor. Permiten tener una clase mucho más limpia.
- Las clases [sealed](https://learn.microsoft.com/es-es/dotnet/csharp/language-reference/keywords/sealed) no pueden ser heredadas por otros objetos.

#### Features

Es una estructura de carpetas que encapsulan la logica relacionada a la entidad. Dentro de esta se encuentra el modelo, el repositorio y el Id. Estos features tambien se pueden agrupar si estos comparten caracteristicas, como es el caso de la entidad *Section (Sección)* y *SubSection (Subsección)*.

#### Entitidades

La estructura de las entidades se mantiene a diferencia de la primary key, la cual la abtraeremos a una clase aparte.

**UserId**

```csharp
/// <summary>
/// User id type.
/// </summary>
public sealed class UserId
    : ValueObject
{
    private UserId(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public string Value { get; private set; }

    /// <summary>
    ///  Returns implicit an instance of <see cref="UserId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator UserId(string value) => ToUserId(value);

    /// <summary>
    /// Create an <see cref="UserId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an <see cref="UserId"/> instance.</returns>
    public static UserId ToUserId(string value)
    {
        return new UserId(value);
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
```

```csharp
/// <summary>
/// User model from the database table.
/// </summary>
public class User
    : Entity<UserId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="password">User password.</param>
    public User(UserId id, string password)
        : base(id)
    {
        Password = password;
    }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s password.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s password.
    /// </value>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s salt.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s salt.
    /// </value>

    public byte[][]? Salt { get; set; }

    /// <summary>
    /// Gets the <see cref="User"/>'s <see cref="IReadOnlyList{T}"/> roles.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s <see cref="IReadOnlyList{T}"/> roles.
    /// </value>
    public virtual ICollection<Role> Roles { get; } = new List<Role>();

    /// <summary>
    /// Add roles to an user.
    /// </summary>
    /// <param name="role"><see cref="Role"/> instance.</param>
    public void Add(Role role)
    {
        Roles.Add(role);
    }
}
```

La misma estructura se replica en las demás entidades. Generamos los demás campos basados en la base de datos.

**NOTA:**

- Se agregó un nuevo campo a la base de datos de tipo *bytea[]* (En la db). Este campo es usado para el método de encriptación [Salting Hash](https://code-maze.com/csharp-hashing-salting-passwords-best-practices/).

#### Repositories

**IRepository:** Es una interfaz que mapea unas operaciones base de la base de datos.

```csharp
/// <summary>
/// Repository base interface.
/// </summary>
/// <typeparam name="TEntity">Entity class.</typeparam>
/// <typeparam name="TEntityId">Entity id class.</typeparam>
public interface IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    /// <summary>
    /// Add entity on the database.
    /// </summary>
    /// <param name="entity">Instance of entity.</param>
    void Add(TEntity entity);

    /// <summary>
    /// Update entity on the database.
    /// </summary>
    /// <param name="entity">Instance of entity.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Delete entity on the database.
    /// </summary>
    /// <param name="entity">Instance of entity.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Gets the entity by id.
    /// </summary>
    /// <param name="id">Entity id.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>Either entity or error instance.</returns>
    Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the entities by bulk ids.
    /// </summary>
    /// <param name="ids">Entity ids.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>Either entity or error instance.</returns>
    Task<IEnumerable<TEntity>> GetBulkIdsAsync(TEntityId[] ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the pagination entities.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="rows">Page size.</param>
    /// <param name="ids">Exclude entity ids.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>Returns either pagination entities or error instance.</returns>
    Task<Pagination<TEntity>> GetPaginationAsync(int page, int rows, TEntityId[] ids, CancellationToken cancellationToken = default);
}
```

**IUnitOfWork:**: Es la implementación del patrón [Unit of Work](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).

```csharp
/// <summary>
/// Unit of work pattern.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Save database changes.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token instance.</param>
    /// <returns>Returns a task.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Start a database transaction.
    /// </summary>
    /// <param name="isolationLevel">Instance of <see cref="IsolationLevel"/>.</param>
    /// <returns>Returns an instance of <see cref="IDbTransaction"/>.</returns>
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel);
}
```

### Application

### Infrastructure

### Persistence

### Presentation

### WebApi
