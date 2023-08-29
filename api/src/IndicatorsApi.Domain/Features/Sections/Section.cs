namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Section model from the database table.
/// </summary>
public sealed class Section
{
    /// <summary>
    /// Gets or sets the <see cref="Section"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="Section"/>'s id.
    /// </value>
    required public int Id { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Section"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Section"/>'s name.
    /// </value>
    required public string Name { get; set; }
}
