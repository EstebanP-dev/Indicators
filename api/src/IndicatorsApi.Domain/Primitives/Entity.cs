namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// Entity base class.
/// </summary>
/// <typeparam name="TEntityId">Id type.</typeparam>
public abstract class Entity<TEntityId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    /// <param name="id">Entity id.</param>
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    protected Entity()
    {
    }

    /// <summary>
    /// Gets or sets and sets the entity id.
    /// </summary>
    /// <value>
    /// The entity id.
    /// </value>
    required public TEntityId Id { get; set; }
}
