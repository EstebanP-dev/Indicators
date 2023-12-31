# Repository

Es la implemnetación de la interfaz [IRepository](./../../domain/repositories/Irepository.md).

**NOTA:**

- La implementación de métodos externos con el context, enviado por parámetro, tienen un mejor rendimiento a la hora de traer los datos desde la base de datos (Usando [EntityFramework](https://learn.microsoft.com/en-us/ef/core/)).
- Los métodos deben ser virtuales para permitir que, las clases hijas, puedan sobre escribir el método.

```csharp
/// <summary>
/// Implementation of <see cref="IRepository{TEntity, TEntityId}"/>.
/// </summary>
/// <typeparam name="TEntity">Entity class.</typeparam>
/// <typeparam name="TEntityId">Entity id class.</typeparam>
internal abstract class Repository<TEntity, TEntityId>
    : IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    /// <summary>
    /// Instance of <see cref="ApplicationDbContext"/>.
    /// </summary>
    protected readonly ApplicationDbContext DbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity, TEntityId}"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    protected Repository(ApplicationDbContext context)
    {
        DbContext = context;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await SingleAsync(
                context: DbContext,
                id: id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public virtual async Task<Pagination<TEntity>> GetPaginationAsync(int page, int rows, TEntityId[] ids, CancellationToken cancellationToken = default)
    {
        int totalRows = await CountAsync(
                context: DbContext,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        int totalPages = PaginationUtils.ConvertTotalPages(
                totalRows: totalRows,
                pageSize: rows);

        List<TEntity> entities = await PaginationAsync(
                context: DbContext,
                page: page,
                rows: rows,
                ids: ids,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        Pagination<TEntity> pagination = new(
            totalPages: totalPages,
            currentPage: page,
            pageSize: rows,
            response: entities);

        return pagination;
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TEntity>> GetBulkIdsAsync(TEntityId[] ids, CancellationToken cancellationToken = default)
    {
        return await BulkAsync(
                context: DbContext,
                ids: ids,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public virtual void Add(TEntity entity) =>
        DbContext
            .Set<TEntity>()
            .Add(entity);

    /// <inheritdoc/>
    public virtual void Delete(TEntity entity) =>
        DbContext
            .Set<TEntity>()
            .Remove(entity);

    /// <inheritdoc/>
    public virtual void Update(TEntity entity) =>
        DbContext
            .Set<TEntity>()
            .Update(entity);

    private static Task<int> CountAsync(ApplicationDbContext context, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .AsSingleQuery()
            .CountAsync(cancellationToken);

    private static Task<List<TEntity>> PaginationAsync(ApplicationDbContext context, int page, int rows, TEntityId[] ids, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .Where(entity => !ids.Any(id => id == entity.Id))
            .AsQueryable()
            .Skip(page * rows)
            .Take(rows)
            .ToListAsync(cancellationToken);

    private static Task<TEntity?> SingleAsync(ApplicationDbContext context, TEntityId id, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .FirstOrDefaultAsync(
                predicate: entity => entity.Id == id,
                cancellationToken: cancellationToken);

    private static Task<List<TEntity>> BulkAsync(ApplicationDbContext context, TEntityId[] ids, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .Where(entity => ids.Any(id => id == entity.Id))
            .AsQueryable()
            .ToListAsync(cancellationToken);
}
```
