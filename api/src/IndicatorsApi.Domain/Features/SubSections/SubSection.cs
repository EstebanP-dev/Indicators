using IndicatorsApi.Domain.Features.Articles;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.SubSections;

/// <summary>
/// SubSection model from the database table.
/// </summary>
public sealed class SubSection
    : Entity<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubSection"/> class.
    /// </summary>
    /// <param name="id">Section id.</param>
    /// <param name="name">Section name.</param>
    public SubSection(string id, string name)
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
    required public string Name { get; set; }
}
