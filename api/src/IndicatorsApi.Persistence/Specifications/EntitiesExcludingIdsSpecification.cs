using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Persistence.Specifications;

/// <inheritdoc/>
internal sealed class EntitiesExcludingIdsSpecification<TEntity, TEntityId>
    : Specification<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntitiesExcludingIdsSpecification{TEntity, TEntityId}"/> class.
    /// </summary>
    /// <param name="excludeIds">Exclude entities.</param>
    public EntitiesExcludingIdsSpecification(TEntityId[] excludeIds)
        : base(entity => !excludeIds.Contains(entity.Id))
    {
    }
}
