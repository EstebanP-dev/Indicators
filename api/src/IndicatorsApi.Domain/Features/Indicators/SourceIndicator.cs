namespace IndicatorsApi.Domain.Features.Indicators;

/// <summary>
/// SourceIndicator model from the database table.
/// </summary>
public sealed class SourceIndicator
{
    /// <summary>
    /// Gets or sets the <see cref="SourceIndicator"/>'s source id.
    /// </summary>
    /// <value>
    /// The <see cref="SourceIndicator"/>'s source id.
    /// </value>
    required public int SourceId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="SourceIndicator"/>'s indicator id.
    /// </summary>
    /// <value>
    /// The <see cref="SourceIndicator"/>'s indicator id.
    /// </value>
    required public int IndicatorId { get; set; }
}
