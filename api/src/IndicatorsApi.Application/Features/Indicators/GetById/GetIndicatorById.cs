using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Contracts.Displays;
using IndicatorsApi.Contracts.Frequencies;
using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Contracts.Meanings;
using IndicatorsApi.Contracts.MeasurementUnits;
using IndicatorsApi.Contracts.Sources;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.GetById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Indicator id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetIndicatorByIdQuery(int Id)
    : IQuery<IndicatorByIdResponse>;

/// <inheritdoc/>
internal sealed class GetIndicatorByIdQueryHandler
    : IQueryHandler<GetIndicatorByIdQuery, IndicatorByIdResponse>
{
    private readonly IIndicatorRepository _repository;
    private readonly IVariableIndicatorRepository _variableIndicatorRepository;
    private readonly IIndicatorResultRepository _indicatorResultRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    /// <param name="variableIndicatorRepository">Instance of <see cref="IVariableIndicatorRepository"/>.</param>
    /// <param name="indicatorResultRepository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    public GetIndicatorByIdQueryHandler(
        IIndicatorRepository repository,
        IVariableIndicatorRepository variableIndicatorRepository,
        IIndicatorResultRepository indicatorResultRepository)
    {
        _repository = repository;
        _variableIndicatorRepository = variableIndicatorRepository;
        _indicatorResultRepository = indicatorResultRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IndicatorByIdResponse>> Handle(GetIndicatorByIdQuery request, CancellationToken cancellationToken)
    {
        Indicator? indicator = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (indicator is null)
        {
            return DomainErrors.NotFound<Indicator>();
        }

        IEnumerable<VariableIndicatorPaginationResponse> variableIndicators = await _variableIndicatorRepository
            .GetAllByIndicatorIdAsync(
                indicatorId: request.Id,
                selector: x => new VariableIndicatorPaginationResponse(x.Id, x.Datum, x.Variable!.Id, x.Variable.Name),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        IEnumerable<IndicatorResultPaginationResponse> indicatorResults = await _indicatorResultRepository
            .GetAllByIndicatorIdAsync(
                indicatorId: request.Id,
                selector: x => new IndicatorResultPaginationResponse(x.Id, x.Result, x.CalculusDate),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        IndicatorByIdResponse response = new(
            Id: indicator.Id,
            Code: indicator.Code,
            Name: indicator.Name,
            Objective: indicator.Objective,
            Scope: indicator.Scope,
            Formula: indicator.Formula,
            Goal: indicator.Goal,
            IndicatorType: indicator.IndicatorType?.Adapt<IndicatorTypePaginationResponse>(),
            MeasurementUnit: indicator.MeasurementUnit?.Adapt<MeasurementUnitPaginationResponse>(),
            Meaning: indicator.Meaning?.Adapt<MeaningPaginationResponse>(),
            Frequency: indicator.Frequency?.Adapt<FrequencyPaginationResponse>(),
            Results: indicatorResults,
            Displays: indicator.Displays.Adapt<IEnumerable<DisplayPaginationResponse>>(),
            Variables: variableIndicators,
            Sources: indicator.Sources.Adapt<IEnumerable<SourceByIdResponse>>(),
            Actors: indicator.Actors.Adapt<IEnumerable<ActorByIdResponse>>());

        return response;
    }
}