using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Meanings;

/// <summary>
/// Meaning model from the database table.
/// </summary>
public sealed class Meaning
    : Entity<MeaningId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Meaning"/> class.
    /// </summary>
    /// <param name="id">Meaning id.</param>
    /// <param name="name">Meaning name.</param>
    public Meaning(MeaningId id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="Meaning"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Meaning"/>'s name.
    /// </value>
    public string Name { get; set; }
}
