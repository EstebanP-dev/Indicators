using System.Linq.Expressions;
using IndicatorsApi.Domain.Models;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;
using IndicatorsApi.Domain.Utils;
using IndicatorsApi.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Persistence.Abstractions;

/// <summary>
/// Implementation of <see cref="IRepository{TEntity, TEntityId}"/>.
/// </summary>
/// <typeparam name="TEntity">Entity class.</typeparam>
/// <typeparam name="TEntityId">Entity id class.</typeparam>
internal abstract class Repository<TEntity, TEntityId>
    : IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
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
    public async Task<bool> DoEntityExistsAsync(TEntityId? id, CancellationToken cancellationToken)
        => await ApplySpecification(new EntityByIdSpecification<TEntity, TEntityId>(id))
        .AsNoTracking()
        .AnyAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <inheritdoc/>
    public async Task<bool> DoEntitiesExistsAsync(TEntityId[] ids, CancellationToken cancellationToken)
        => await ApplySpecification(new EntitiesByIdsSpecification<TEntity, TEntityId>(ids))
        .AsNoTracking()
        .AnyAsync(x => !ids.Contains(x.Id), cancellationToken)
        .ConfigureAwait(false);

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(
            TEntityId? id,
            CancellationToken cancellationToken = default)
        => await ApplySpecification(new EntityByIdSpecification<TEntity, TEntityId>(id))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

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
    public virtual async Task<Pagination<TResponse>> GetPaginationAsync<TResponse>(
        PaginationParameters<TEntityId> parameters,
        Expression<Func<TEntity, TResponse>> selector,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        int totalRows = await CountAsync(
                context: DbContext,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        int totalPages = PaginationUtils.ConvertTotalPages(
                totalRows: totalRows,
                pageSize: parameters.Rows);

        List<TResponse> entities = await PaginationAsync<TResponse>(
                context: DbContext,
                parameters: parameters,
                selector: selector,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        Pagination<TResponse> pagination = new(
            totalRows,
            totalPages: totalPages,
            currentPage: parameters.Page,
            pageSize: parameters.Rows,
            response: entities);

        return pagination;
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TEntity>> GetBulkIdsAsync(
            TEntityId[] ids,
            CancellationToken cancellationToken = default)
        => await ApplySpecification(new EntitiesByIdsSpecification<TEntity, TEntityId>(ids))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

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
            .Add(entity);

    /// <inheritdoc/>
    public virtual void Delete(TEntity entity) =>
        DbContext
            .Remove(entity);

    /// <inheritdoc/>
    public virtual void Update(TEntity entity) =>
        DbContext
            .Set<TEntity>()
            .Update(entity);

    /// <summary>
    /// Apply a specification implementation.
    /// </summary>
    /// <param name="specification">Specification instance.</param>
    /// <returns>Returns a query with the specification implemented.</returns>
    protected IQueryable<TEntity> ApplySpecification(
        Specification<TEntity, TEntityId> specification)
    {
        return SpecificationEvaluator.GetQuery(
            DbContext.Set<TEntity>().AsNoTracking(),
            specification);
    }

    private static Task<int> CountAsync(ApplicationDbContext context, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .AsNoTracking()
            .AsSingleQuery()
            .CountAsync(cancellationToken);

    private static Task<List<TEntity>> PaginationAsync(
            ApplicationDbContext context,
            int page,
            int rows,
            TEntityId[] ids,
            CancellationToken cancellationToken = default) => context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(entity => !ids.Contains(entity.Id))
            .Skip(page * rows)
            .Take(rows)
            .ToListAsync(cancellationToken);

    private static Task<List<TResponse>> PaginationAsync<TResponse>(
            ApplicationDbContext context,
            PaginationParameters<TEntityId> parameters,
            Expression<Func<TEntity, TResponse>> selector,
            CancellationToken cancellationToken = default)
        where TResponse : class
        => context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(
                entity => !(parameters.Excludes ?? Array.Empty<TEntityId>())
                    .Contains(entity.Id))
            .Select(selector: selector)
            .Skip(parameters.Page * parameters.Rows)
            .Take(parameters.Rows)
            .ToListAsync(cancellationToken);

    private static Task<List<TEntity>> AllAsync(ApplicationDbContext context, TEntityId[] ids, CancellationToken cancellationToken = default) =>
        context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(entity => !ids.Any(id => id!.Equals(entity.Id)))
            .ToListAsync(cancellationToken);
}
