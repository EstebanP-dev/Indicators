using IndicatorsApi.Domain.Features.Articles;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Section model from the database table.
/// </summary>
public sealed class Section
    : Entity<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Section"/> class.
    /// </summary>
    /// <param name="id">Section id.</param>
    public Section(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Section"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Section"/>'s name.
    /// </value>
    required public string Name { get; set; }
}
