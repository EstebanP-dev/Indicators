using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Persistence.Repositories;

/// <summary>
/// Implementation of <see cref="IRepository{TEntity}"/>.
/// </summary>
/// <typeparam name="TEntity">Entity class.</typeparam>
internal abstract class Repository<TEntity>
    : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Instance of <see cref="ApplicationDbContext"/>.
    /// </summary>
#pragma warning disable SA1401 // Fields should be private
    protected readonly ApplicationDbContext DbContext;
#pragma warning restore SA1401 // Fields should be private

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    protected Repository(ApplicationDbContext context)
    {
        DbContext = context;
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
}
