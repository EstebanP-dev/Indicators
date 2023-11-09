using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Indicators;

/// <summary>
/// DisplayIndicator model from the database table.
/// </summary>
public sealed class DisplayIndicator
    : Entity<int>
{
    /// <summary>
    /// Gets or sets the <see cref="DisplayIndicator"/>'s display id.
    /// </summary>
    /// <value>
    /// The <see cref="DisplayIndicator"/>'s display id.
    /// </value>
    required public int DisplayId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DisplayIndicator"/>'s indicator id.
    /// </summary>
    /// <value>
    /// The <see cref="DisplayIndicator"/>'s indicator id.
    /// </value>
    required public int IndicatorId { get; set; }
}
