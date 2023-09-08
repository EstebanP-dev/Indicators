# IUserRepository

Es la abstracción del comportamiento de la clase [User](./user.md). Esta interfaz, permite tener las acciones a realizar en la base de datos.

```csharp
/// <summary>
/// User repository methods.
/// </summary>
public interface IUserRepository
    : IRepository<User, UserId>
{
}
```
