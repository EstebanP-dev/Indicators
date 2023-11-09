# IUnitOfWork

Es la implementación del patrón [Unit of Work](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).

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
