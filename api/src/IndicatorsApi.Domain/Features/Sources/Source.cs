using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sources;

/// <summary>
/// Source model from the database table.
/// </summary>
public sealed class Source
    : Entity<SourceId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Source"/> class.
    /// </summary>
    /// <param name="id">Source id.</param>
    /// <param name="name">Source name.</param>
    public Source(SourceId id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="Role"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s name.
    /// </value>
    public string Name { get; set; }
}
