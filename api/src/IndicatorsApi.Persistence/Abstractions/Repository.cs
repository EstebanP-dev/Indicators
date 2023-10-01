using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;
using IndicatorsApi.Domain.Utils;

namespace IndicatorsApi.Persistence.Abstractions;

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
#pragma warning disable SA1401 // Fields should be private
    protected readonly ApplicationDbContext DbContext;
#pragma warning restore SA1401 // Fields should be private

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
            totalRows,
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
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TEntityId[] ids, CancellationToken cancellationToken = default)
    {
        return await AllAsync(
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
            .AsNoTracking()
            .AsSingleQuery()
            .CountAsync(cancellationToken);

    private static Task<List<TEntity>> PaginationAsync(ApplicationDbContext context, int page, int rows, TEntityId[] ids, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(entity => !ids.Any(id => id == entity.Id))
            .Skip(page * rows)
            .Take(rows)
            .ToListAsync(cancellationToken);

    private static Task<TEntity?> SingleAsync(ApplicationDbContext context, TEntityId id, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                predicate: entity => entity.Id == id,
                cancellationToken: cancellationToken);

    private static Task<List<TEntity>> BulkAsync(ApplicationDbContext context, TEntityId[] ids, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(entity => ids.Any(id => id == entity.Id))
            .ToListAsync(cancellationToken);

    private static Task<List<TEntity>> AllAsync(ApplicationDbContext context, TEntityId[] ids, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(entity => !ids.Any(id => id == entity.Id))
            .ToListAsync(cancellationToken);
}
