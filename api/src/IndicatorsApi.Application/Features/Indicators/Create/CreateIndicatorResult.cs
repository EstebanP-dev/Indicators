using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.Create;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Result">IndicatorResult result.</param>
/// <param name="CalculusDate">IndicatorResult calculus date.</param>
/// <param name="IndicatorId">IndicatorResult indicator id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorResultCommand(double Result, DateTime CalculusDate, int IndicatorId)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateIndicatorResultValidator : AbstractValidator<CreateIndicatorResultCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIndicatorResultValidator"/> class.
    /// </summary>
    public CreateIndicatorResultValidator()
    {
        RuleFor(x => x.Result)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.CalculusDate)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.IndicatorId)
            .NotNull()
            .NotEmpty();
    }
}

/// <inheritdoc/>
internal sealed class CreateIndicatorResultCommandHandler
    : ICommandHandler<CreateIndicatorResultCommand, Created>
{
    private readonly IIndicatorResultRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIndicatorResultCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateIndicatorResultCommandHandler(IIndicatorResultRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateIndicatorResultCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: request.Adapt<IndicatorResult>());

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Created;
    }
}