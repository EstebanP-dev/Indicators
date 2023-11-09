using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Persistence.Specifications;

/// <inheritdoc/>
internal sealed class EntitiesByIdsSpecification<TEntity, TEntityId>
    : Specification<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntitiesByIdsSpecification{TEntity, TEntityId}"/> class.
    /// </summary>
    /// <param name="ids">Entity id instance.</param>
    internal EntitiesByIdsSpecification(TEntityId[] ids)
        : base(entity => ids.Contains(entity.Id))
    {
    }
}
