# UnitOfWork

Es la implementación de la interfaz [IUnitOfWork](./../domain/repositories/IunitOfWork.md).

```csharp
/// <inheritdoc/>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
    {
        IDbContextTransaction transaction = _context.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }

    /// <inheritdoc/>
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
```
