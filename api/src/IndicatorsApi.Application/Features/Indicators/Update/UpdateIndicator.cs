using System.Collections.ObjectModel;
using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Application.Features.Indicators.Update;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Indicator id.</param>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
/// <param name="Objective">Indicator objective.</param>
/// <param name="Scope">Indicator scoped.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorTypeId">Indicator type id.</param>
/// <param name="MeasurementUnitId">Indicator measurement unit id.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="MeaningId">Indicator meaning id.</param>
/// <param name="FrequencyId">Indicator frecuency id.</param>
/// <param name="Results">Indicator results.</param>
/// <param name="Displays">Indicator displays.</param>
/// <param name="Sources">Indicator sources.</param>
/// <param name="Actors">Indicator actors.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateIndicatorCommand(
    int Id,
    string? Code,
    string? Name,
    string? Objective,
    string? Scope,
    string? Formula,
    int? IndicatorTypeId,
    int? MeasurementUnitId,
    string? Goal,
    int? MeaningId,
    int? FrequencyId,
    IEnumerable<int>? Results,
    IEnumerable<int>? Displays,
    IEnumerable<int>? Sources,
    IEnumerable<string>? Actors)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateIndicatorCommandHandler
    : ICommandHandler<UpdateIndicatorCommand, Updated>
{
    private readonly IIndicatorRepository _repository;
    private readonly IDisplayRepository _displayRepository;
    private readonly ISourceRepository _sourceRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    /// <param name="displayRepository">Instance of <see cref="IDisplayRepository"/>.</param>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    /// <param name="actorRepository">Instance of <see cref="IActorRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateIndicatorCommandHandler(
        IIndicatorRepository repository,
        IDisplayRepository displayRepository,
        ISourceRepository sourceRepository,
        IActorRepository actorRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _displayRepository = displayRepository;
        _sourceRepository = sourceRepository;
        _actorRepository = actorRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateIndicatorCommand request, CancellationToken cancellationToken)
    {
        List<Task<ErrorOr<Success>>> bulkGetTaskErrors = new();

        Indicator? indicator = await _repository
            .GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (indicator is null)
        {
            return DomainErrors.NotFound<Indicator>();
        }

        indicator.UpdateIndicatorValues(
            request.Code,
            request.Name,
            request.Objective,
            request.Scope,
            request.Formula,
            request.Goal,
            request.IndicatorTypeId,
            request.MeasurementUnitId,
            request.MeaningId,
            request.FrequencyId);

        bulkGetTaskErrors.Add(
            indicator.TryAddEntities(
                _displayRepository,
                new Collection<int>(request.Displays?.ToList() ?? indicator.Displays.Select(x => x.Id).ToList()),
                indicator.AddDisplay,
                indicator.RemoveDisplay,
                indicator.ClearDisplays,
                cancellationToken));

        bulkGetTaskErrors.Add(
            indicator.TryAddEntities(
                _sourceRepository,
                new Collection<int>(request.Sources?.ToList() ?? indicator.Sources.Select(x => x.Id).ToList()),
                indicator.AddSource,
                indicator.RemoveSource,
                indicator.ClearSources,
                cancellationToken));

        bulkGetTaskErrors.Add(
            indicator.TryAddEntities(
                _actorRepository,
                new Collection<string>(request.Actors?.ToList() ?? indicator.Actors.Select(x => x.Id).ToList()),
                indicator.AddActors,
                indicator.RemoveActor,
                indicator.ClearDisplays,
                cancellationToken));

        await Task.WhenAll(bulkGetTaskErrors).ConfigureAwait(false);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}
