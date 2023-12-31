# IRepository

Es una interfaz que mapea unas operaciones base de la base de datos.

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