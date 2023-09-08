using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.ActorTypes;

/// <summary>
/// ActorType model from the database table.
/// </summary>
public sealed class ActorType
    : Entity<ActorTypeId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActorType"/> class.
    /// </summary>
    /// <param name="id">ActorType id.</param>
    /// <param name="name">ActorType name.</param>
    public ActorType(ActorTypeId id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="ActorType"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="ActorType"/>'s name.
    /// </value>
    public string Name { get; set; }
}
