using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.UpdateSection;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Indicator id.</param>
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
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateIndicatorCommand(
    int Id,
    string Code,
    string Name,
    string Objective,
    string Scoped,
    string Formula,
    int IndicatorTypeId,
    int MeasurementUnitId,
    string Goal,
    int MeaningId,
    int FrequencyId)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateIndicatorValidator : AbstractValidator<UpdateIndicatorCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    public UpdateIndicatorValidator(IIndicatorRepository repository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Id)
            .MustAsync(async (id, _) =>
            {
                return await repository
                    .DoEntityExistsAsync(id, default)
                    .ConfigureAwait(false);
            })
            .WithMessage(DomainErrors.NotFound<Indicator>().Description);

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(200);
    }
}

/// <inheritdoc/>
internal sealed class UpdateIndicatorCommandHandler
    : ICommandHandler<UpdateIndicatorCommand, Updated>
{
    private readonly IIndicatorRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateIndicatorCommandHandler(IIndicatorRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateIndicatorCommand request, CancellationToken cancellationToken)
    {
        Indicator? indicator = await _repository
                    .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

        if (indicator is null)
        {
            return DomainErrors.NotFound<Indicator>();
        }

        Indicator newIndicator = request.Adapt<Indicator>();
        newIndicator.Id = request.Id;

        _repository.Update(entity: newIndicator);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}
