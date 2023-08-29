namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// SubSection model from the database table.
/// </summary>
public sealed class SubSection
{
    /// <summary>
    /// Gets or sets the <see cref="SubSection"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="SubSection"/>'s id.
    /// </value>
    required public int Id { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="SubSection"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="SubSection"/>'s name.
    /// </value>
    required public string Name { get; set; }
}
