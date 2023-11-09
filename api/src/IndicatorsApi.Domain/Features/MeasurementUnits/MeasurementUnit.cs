using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.MeasurementUnits;

/// <summary>
/// MeasurementUnit model from the database table.
/// </summary>
public sealed class MeasurementUnit
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeasurementUnit"/> class.
    /// </summary>
    /// <param name="id">MeasurementUnit id.</param>
    /// <param name="description">MeasurementUnit description.</param>
    public MeasurementUnit(int id, string description)
        : base(id)
    {
        Description = description;
    }

    /// <summary>
    /// Gets or sets the <see cref="MeasurementUnit"/>'s description.
    /// </summary>
    /// <value>
    /// The <see cref="MeasurementUnit"/>'s description.
    /// </value>
    public string Description { get; set; }
}
