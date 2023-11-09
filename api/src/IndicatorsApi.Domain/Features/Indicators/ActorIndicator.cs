using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Indicators;

/// <summary>
/// ActorIndicator model from the database table.
/// </summary>
public sealed class ActorIndicator
{
    /// <summary>
    /// Gets or sets the <see cref="ActorIndicator"/>'s variable id.
    /// </summary>
    /// <value>
    /// The <see cref="ActorIndicator"/>'s variable id.
    /// </value>
    required public string ActorId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="ActorIndicator"/>'s indicator id.
    /// </summary>
    /// <value>
    /// The <see cref="ActorIndicator"/>'s indicator id.
    /// </value>
    required public int IndicatorId { get; set; }

    /// <summary>
    /// Gets the <see cref="ActorIndicator"/>'s date.
    /// </summary>
    /// <value>
    /// The <see cref="ActorIndicator"/>'s date.
    /// </value>
    public DateTime Date { get; }

    /// <summary>
    /// Gets the <see cref="ActorIndicator"/>'s <see cref="Actor"/>.
    /// </summary>
    /// <value>
    /// The <see cref="ActorIndicator"/>'s <see cref="Actor"/>.
    /// </value>
    public Actor? Actor { get; }
}
