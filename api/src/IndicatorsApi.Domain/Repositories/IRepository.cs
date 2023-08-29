namespace IndicatorsApi.Domain.Repositories;

/// <summary>
/// Repository base interface.
/// </summary>
/// <typeparam name="TEntity">Entity class.</typeparam>
public interface IRepository<in TEntity>
    where TEntity : class
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
}
