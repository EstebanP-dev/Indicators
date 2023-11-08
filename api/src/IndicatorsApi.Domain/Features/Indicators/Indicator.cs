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
    /// Initializes a new instance of the <see cref="Indicator"/> class.
    /// </summary>
    public Indicator()
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
    /// Gets the <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Display"/>s.
    /// </summary>
    /// <value>
    /// The <see cref="Indicator"/>'s <see cref="ICollection{T}"/> <see cref="Display"/>s.
    /// </value>
    public ICollection<Display> Displays { get; } = new List<Display>();

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
    /// Update the <see cref="Indicator"/> values.
    /// </summary>
    /// <param name="code"><see cref="Code"/>.</param>
    /// <param name="name"><see cref="Name"/>.</param>
    /// <param name="objective"><see cref="Objective"/>.</param>
    /// <param name="scope"><see cref="Scope"/>.</param>
    /// <param name="formula"><see cref="Formula"/>.</param>
    /// <param name="goal"><see cref="Goal"/>.</param>
    /// <param name="indicatorTypeId"><see cref="IndicatorTypeId"/>.</param>
    /// <param name="measurementUnitId"><see cref="MeasurementUnitId"/>.</param>
    /// <param name="meaningId"><see cref="MeaningId"/>.</param>
    /// <param name="frequencyId"><see cref="FrequencyId"/>.</param>
    public void UpdateIndicatorValues(
        string? code,
        string? name,
        string? objective,
        string? scope,
        string? formula,
        string? goal,
        int? indicatorTypeId,
        int? measurementUnitId,
        int? meaningId,
        int? frequencyId)
    {
        if (code is not null)
        {
            Code = code;
        }

        if (name is not null)
        {
            Name = name;
        }

        if (objective is not null)
        {
            Objective = objective;
        }

        if (scope is not null)
        {
            Scope = scope;
        }

        if (formula is not null)
        {
            Formula = formula;
        }

        if (goal is not null)
        {
            Goal = goal;
        }

        if (indicatorTypeId is not null)
        {
            IndicatorTypeId = indicatorTypeId.Value;
        }

        if (measurementUnitId is not null)
        {
            MeasurementUnitId = measurementUnitId.Value;
        }

        if (meaningId is not null)
        {
            MeaningId = meaningId.Value;
        }

        if (frequencyId is not null)
        {
            FrequencyId = frequencyId.Value;
        }
    }

    /// <summary>
    /// Add display to an indicator.
    /// </summary>
    /// <param name="display"><see cref="Display"/> instance.</param>
    public void AddDisplay(Display display)
    {
        if (!Displays.Any(x => x.Id == display.Id))
        {
            Displays.Add(display);
        }
    }

    /// <summary>
    /// Add source to an indicator.
    /// </summary>
    /// <param name="source"><see cref="Source"/> instance.</param>
    public void AddSource(Source source)
    {
        if (!Sources.Any(x => x.Id == source.Id))
        {
            Sources.Add(source);
        }
    }

    /// <summary>
    /// Add actor to an indicator.
    /// </summary>
    /// <param name="actor"><see cref="Actors"/> instance.</param>
    public void AddActors(Actor actor)
    {
        if (!Actors.Any(x => x.Id == actor.Id))
        {
            Actors.Add(actor);
        }
    }
}
