using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Features.Frequencies;
using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.Variables;
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

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="IndicatorResult"/>s.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="IndicatorResult"/>s.
    /// </value>
    public ICollection<IndicatorResult> Results { get; } = new List<IndicatorResult>();

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Display"/>s.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Display"/>s.
    /// </value>
    public ICollection<Display> Displays { get; } = new List<Display>();

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="VariableIndicator"/>s.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="VariableIndicator"/>s.
    /// </value>
    public ICollection<VariableIndicator> Variables { get; } = new List<VariableIndicator>();

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Source"/>s.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Source"/>s.
    /// </value>
    public ICollection<Source> Sources { get; } = new List<Source>();

    /// <summary>
    /// Gets the <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Actor"/>s.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Actor"/>s.
    /// </value>
    public ICollection<Actor> Actors { get; } = new List<Actor>();

    /// <summary>
    /// Add indicator result to an indicator.
    /// </summary>
    /// <param name="result"><see cref="IndicatorResult"/> instance.</param>
    public void AddResult(IndicatorResult result)
    {
        Results.Add(result);
    }

    /// <summary>
    /// Add display to an indicator.
    /// </summary>
    /// <param name="display"><see cref="Display"/> instance.</param>
    public void AddDisplay(Display display)
    {
        Displays.Add(display);
    }

    /// <summary>
    /// Add variable to an indicator.
    /// </summary>
    /// <param name="variable"><see cref="VariableIndicator"/> instance.</param>
    public void AddVariable(VariableIndicator variable)
    {
        Variables.Add(variable);
    }

    /// <summary>
    /// Add source to an indicator.
    /// </summary>
    /// <param name="source"><see cref="Source"/> instance.</param>
    public void AddSource(Source source)
    {
        Sources.Add(source);
    }

    /// <summary>
    /// Add actor to an indicator.
    /// </summary>
    /// <param name="actor"><see cref="Actors"/> instance.</param>
    public void AddActors(Actor actor)
    {
        Actors.Add(actor);
    }
}
