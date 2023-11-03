using IndicatorsApi.Contracts.Frequencies;
using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Contracts.Meanings;
using IndicatorsApi.Contracts.MeasurementUnits;
using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Features.Frequencies;
using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Application.Features.Indicators.UpdateSection;

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
    string Code,
    string Name,
    string Objective,
    string Scope,
    string Formula,
    int IndicatorTypeId,
    int MeasurementUnitId,
    string Goal,
    int MeaningId,
    int FrequencyId,
    IEnumerable<int> Results,
    IEnumerable<int> Displays,
    IEnumerable<int> Sources,
    IEnumerable<string> Actors)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateIndicatorValidator : AbstractValidator<UpdateIndicatorCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorValidator"/> class.
    /// </summary>
    /// <param name="indicatorTypeRepository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    /// <param name="measurementUnitRepository">Instance of <see cref="IMeasurementUnitRepository"/>.</param>
    /// <param name="meaningRepository">Instance of <see cref="IMeaningRepository"/>.</param>
    /// <param name="frequencyRepository">Instance of <see cref="IFrequencyRepository"/>.</param>
    public UpdateIndicatorValidator(
        IIndicatorTypeRepository indicatorTypeRepository,
        IMeasurementUnitRepository measurementUnitRepository,
        IMeaningRepository meaningRepository,
        IFrequencyRepository frequencyRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Code)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Objective)
            .NotNull()
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.Scope)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Formula)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.IndicatorTypeId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.IndicatorTypeId)
            .MustAsync(indicatorTypeRepository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<IndicatorType>().Description);

        RuleFor(x => x.MeasurementUnitId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.MeasurementUnitId)
            .MustAsync(measurementUnitRepository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<MeasurementUnit>().Description);

        RuleFor(x => x.Goal)
            .NotNull()
            .NotEmpty()
        .MaximumLength(1000);
        RuleFor(x => x.MeaningId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.MeaningId)
           .MustAsync(meaningRepository.DoEntityExistsAsync)
           .WithMessage(DomainErrors.NotFound<Meaning>().Description);
        RuleFor(x => x.FrequencyId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.FrequencyId)
           .MustAsync(frequencyRepository.DoEntityExistsAsync)
           .WithMessage(DomainErrors.NotFound<Frequency>().Description);
    }
}

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

        indicator.Id = request.Id;
        indicator.Code = request.Code;
        indicator.Name = request.Name;
        indicator.Objective = request.Objective;
        indicator.Scope = request.Scope;
        indicator.Formula = request.Formula;
        indicator.Goal = request.Goal;
        indicator.IndicatorTypeId = request.IndicatorTypeId;
        indicator.MeasurementUnitId = request.MeasurementUnitId;
        indicator.MeaningId = request.MeaningId;
        indicator.FrequencyId = request.FrequencyId;

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        bulkGetTaskErrors.Add(
            indicator.TryAddEntities(_displayRepository, request.Displays, indicator.AddDisplay, cancellationToken));

        bulkGetTaskErrors.Add(
            indicator.TryAddEntities(_sourceRepository, request.Sources, indicator.AddSource, cancellationToken));

        bulkGetTaskErrors.Add(
            indicator.TryAddEntities(_actorRepository, request.Actors, indicator.AddActors, cancellationToken));

        IEnumerable<ErrorOr<Success>> bulkGetErrors = await Task.WhenAll(bulkGetTaskErrors).ConfigureAwait(false);
        IEnumerable<ErrorOr<Success>> errors = bulkGetErrors.Where(x => x.IsError).AsEnumerable();

        if (errors.Any())
        {
            return errors.FirstOrDefault().FirstError;
        }

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}
