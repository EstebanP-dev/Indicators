using IndicatorsApi.Domain.Features.Frequencies;
using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Indicators;

/// <summary>
/// Indicator repository methods.
/// </summary>
public interface IIndicatorRepository
    : IRepository<Indicator, int>
{
}

/// <summary>
/// Indicator model from the database table.
/// </summary>
public sealed class Indicator
    : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Indicator"/> class.
    /// </summary>
    /// <param name="id">Indicator id.</param>
    public Indicator(int id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s code.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s code.
    /// </value>
    required public string Code { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s name.
    /// </value>
    required public string Name { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s objective.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s objective.
    /// </value>
    required public string Objective { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s scope.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s scope.
    /// </value>
    required public string Scope { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s formula.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s formula.
    /// </value>
    required public string Formula { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s indicator type id.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s indicator type id.
    /// </value>
    required public int IndicatorTypeId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s meanesurement unit id.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s meanesurement unit id.
    /// </value>
    required public int MeasurementUnitId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s goal.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s goal.
    /// </value>
    required public string Goal { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s meaning id.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s meaning id.
    /// </value>
    required public int MeaningId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Indicator"/>'s frecuency id.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s frecuency id.
    /// </value>
    required public int FrequencyId { get; set; }

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="IndicatorType"/> instance.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="IndicatorType"/> instance.
    /// </value>
    public IndicatorType? IndicatorType { get; }

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="MeasurementUnit"/> instance.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="MeasurementUnit"/> instance.
    /// </value>
    public MeasurementUnit? MeasurementUnit { get; }

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="Meaning"/> instance.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="Meaning"/> instance.
    /// </value>
    public Meaning? Meaning { get; }

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="Frequency"/> instance.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="Frequency"/> instance.
    /// </value>
    public Frequency? Frequency { get; }
}
