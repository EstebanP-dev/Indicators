using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Actors;

/// <summary>
/// ActorType model from the database table.
/// </summary>
public sealed class Actor
    : Entity<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Actor"/> class.
    /// </summary>
    /// <param name="id">ActorType id.</param>
    /// <param name="name">ActorType name.</param>
    public Actor(string id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="Actor"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Actor"/>'s name.
    /// </value>
    required public string Name { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Actor"/>'s type id.
    /// </summary>
    /// <value>
    /// The <see cref="Actor"/>'s type id.
    /// </value>
    required public int ActorTypeId { get; set; }

    /// <summary>
    /// Gets the <see cref="ActorType"/>.
    /// </summary>
    /// <value>
    /// The <see cref="ActorType"/>.
    /// </value>
    public ActorType? ActorType { get; }
}