using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// SubSection model from the database table.
/// </summary>
public sealed class SubSection
    : Entity<SubSectionId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubSection"/> class.
    /// </summary>
    /// <param name="id">Section id.</param>
    /// <param name="name">Section name.</param>
    public SubSection(SubSectionId id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="SubSection"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="SubSection"/>'s name.
    /// </value>
    public string Name { get; set; }
}
