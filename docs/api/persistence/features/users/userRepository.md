# User Repository

Es la implementación de la interfaz [IUserRepository](./../../../domain/entities/IuserRepository.md).

```csharp
/// <inheritdoc/>
internal sealed class UserRepository
    : Repository<User, UserId>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public UserRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public override async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users
            .Include(user => user.Roles)
            .IgnoreAutoIncludes()
            .AsSingleQuery()
            .FirstOrDefaultAsync(
                predicate: user => user.Id == id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
```

**NOTA:**

- Es necesario sobre escribir el método **GetById** para poder incluir los roles en la consulta.
