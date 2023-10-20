using FluentValidation;
using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Features.Variables;

namespace IndicatorsApi.Application.Features.Indicators.Create;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
/// <param name="Objective">Indicator objective.</param>
/// <param name="Scoped">Indicator scoped.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorTypeId">Indicator type id.</param>
/// <param name="MeasurementUnitId">Indicator measurement unit id.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="MeaningId">Indicator meaning id.</param>
/// <param name="FrequencyId">Indicator frecuency id.</param>
/// <param name="Displays">Indicator displays.</param>
/// <param name="Variables">Indicator variables.</param>
/// <param name="Sources">Indicator sources.</param>
/// <param name="Actors">Indicator actors.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorCommand(
        string Code,
        string Name,
        string Objective,
        string Scoped,
        string Formula,
        int IndicatorTypeId,
        int MeasurementUnitId,
        string Goal,
        int MeaningId,
        int FrequencyId,
        IEnumerable<int> Displays,
        IEnumerable<CreateVariableIndicatorRequest> Variables,
        IEnumerable<int> Sources,
        IEnumerable<string> Actors)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateVariableIndicatorRequestValidator
    : AbstractValidator<CreateVariableIndicatorRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVariableIndicatorRequestValidator"/> class.
    /// </summary>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="variableRepository">Instance of <see cref="IVariableRepository"/>.</param>
    public CreateVariableIndicatorRequestValidator(
            IUserRepository userRepository,
            IVariableRepository variableRepository)
    {
        RuleFor(x => x.UserId)
            .MustAsync(userRepository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<User>().Description);

        RuleFor(x => x.VariableId)
            .MustAsync(variableRepository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<Variable>().Description);
    }
}

/// <inheritdoc/>
internal sealed class CreateIndicatorValidator : AbstractValidator<CreateIndicatorCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIndicatorValidator"/> class.
    /// </summary>
    /// <param name="displayRepository">Instance of <see cref="IDisplayRepository"/>.</param>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    /// <param name="actorRepository">Instance of <see cref="IActorRepository"/>.</param>
    public CreateIndicatorValidator(
            IDisplayRepository displayRepository,
            ISourceRepository sourceRepository,
            IActorRepository actorRepository)
    {
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

        RuleFor(x => x.Scoped)
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

        RuleFor(x => x.MeasurementUnitId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Goal)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.MeaningId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.FrequencyId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Displays)
            .MustAsync((ids, cancellationToken) => displayRepository.DoEntitiesExistsAsync(ids.ToArray(), cancellationToken));

        RuleFor(x => x.Sources)
            .MustAsync((ids, cancellationToken) => sourceRepository.DoEntitiesExistsAsync(ids.ToArray(), cancellationToken));

        RuleFor(x => x.Actors)
            .MustAsync((ids, cancellationToken) => actorRepository.DoEntitiesExistsAsync(ids.ToArray(), cancellationToken));
    }
}

/// <inheritdoc/>
internal sealed class CreateIndicatorCommandHandler
    : ICommandHandler<CreateIndicatorCommand, Created>
{
    private readonly IIndicatorRepository _repository;
    private readonly IDisplayRepository _displayRepository;
    private readonly ISourceRepository _sourceRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IVariableIndicatorRepository _variableIndicatorRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIndicatorCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    /// <param name="displayRepository">Instance of <see cref="IDisplayRepository"/>.</param>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    /// <param name="actorRepository">Instance of <see cref="IActorRepository"/>.</param>
    /// <param name="variableIndicatorRepository">Instance of <see cref="IVariableIndicatorRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateIndicatorCommandHandler(
            IIndicatorRepository repository,
            IDisplayRepository displayRepository,
            ISourceRepository sourceRepository,
            IActorRepository actorRepository,
            IVariableIndicatorRepository variableIndicatorRepository,
            IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _displayRepository = displayRepository;
        _sourceRepository = sourceRepository;
        _actorRepository = actorRepository;
        _variableIndicatorRepository = variableIndicatorRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateIndicatorCommand request, CancellationToken cancellationToken)
    {
        Indicator indicator = request.Adapt<Indicator>();

        IEnumerable<Display> displays = await _displayRepository
            .GetBulkIdsAsync(request.Displays.ToArray(), cancellationToken)
            .ConfigureAwait(false);

        if (displays.Any(x => !request.Displays.Contains(x.Id)))
        {
            return DomainErrors.BulkNotFound;
        }

        foreach (Display display in displays)
        {
            indicator.AddDisplay(display);
        }

        IEnumerable<Source> sources = await _sourceRepository
            .GetBulkIdsAsync(request.Sources.ToArray(), cancellationToken)
            .ConfigureAwait(false);

        if (sources.Any(x => !request.Sources.Contains(x.Id)))
        {
            return DomainErrors.BulkNotFound;
        }

        foreach(Source source in sources)
        {
            indicator.AddSource(source);
        }

        IEnumerable<Actor> actors = await _actorRepository
            .GetBulkIdsAsync(request.Actors.ToArray(), cancellationToken)
            .ConfigureAwait(false);

        if (actors.Any(x => !request.Actors.Contains(x.Id)))
        {
            return DomainErrors.BulkNotFound;
        }

        foreach (Actor actor in actors)
        {
            indicator.AddActors(actor);
        }

        _repository.Add(entity: indicator);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        IEnumerable<VariableIndicator> variableIndicators = request.Variables
                .Select(x =>
                {
                    VariableIndicator variableIndicator = x.Adapt<VariableIndicator>();
                    variableIndicator.IndicatorId = indicator.Id;

                    return variableIndicator;
                });

        foreach (VariableIndicator variableIndicator in variableIndicators)
        {
            _variableIndicatorRepository.Add(variableIndicator);
        }

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Created;
    }
}