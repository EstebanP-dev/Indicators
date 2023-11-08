using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Persistence.Specifications;

/// <inheritdoc/>
internal sealed class EntityByIdSpecification<TEntity, TEntityId>
    : Specification<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityByIdSpecification{TEntity, TEntityId}"/> class.
    /// </summary>
    /// <param name="id">Entity id instance.</param>
    internal EntityByIdSpecification(TEntityId? id)
        : base(entity => id != null && entity.Id != null && entity.Id.Equals(id))
    {
    }
}
