using System.Linq.Expressions;
using IndicatorsApi.Domain.Models;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Repositories;

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
    /// Gets all entities.
    /// </summary>
    /// <param name="ids">Exclude entity ids.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>Either list of entities or error instance.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(TEntityId[] ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the pagination entities.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="rows">Page size.</param>
    /// <param name="ids">Exclude entity ids.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>Returns either pagination entities or error instance.</returns>
    Task<Pagination<TEntity>> GetPaginationAsync(int page, int rows, TEntityId[] ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the pagination entities.
    /// </summary>
    /// <typeparam name="TResponse">Response model type.</typeparam>
    /// <param name="parameters">Pagination parameters.</param>
    /// <param name="selector">Selection function.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>Returns either a list of response model or error instance.</returns>
    Task<Pagination<TResponse>> GetPaginationAsync<TResponse>(PaginationParameters<TEntityId> parameters, Expression<Func<TEntity, TResponse>> selector, CancellationToken cancellationToken = default)
        where TResponse : class;
}
