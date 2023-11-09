using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Indicators.Update;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">IndicatorResult id.</param>
/// <param name="Result">IndicatorResult result.</param>
/// <param name="CalculusDate">IndicatorResult calculus date.</param>
/// <param name="IndicatorId">IndicatorResult indicator id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateIndicatorResultCommand(int Id, double Result, DateTime CalculusDate, int IndicatorId)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateIndicatorResultValidator : AbstractValidator<UpdateIndicatorResultCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorResultValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    public UpdateIndicatorResultValidator(IIndicatorResultRepository repository)
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
            .WithMessage(DomainErrors.NotFound<IndicatorResult>().Description);

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
internal sealed class UpdateIndicatorResultCommandHandler
    : ICommandHandler<UpdateIndicatorResultCommand, Updated>
{
    private readonly IIndicatorResultRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorResultCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateIndicatorResultCommandHandler(IIndicatorResultRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateIndicatorResultCommand request, CancellationToken cancellationToken)
    {
        IndicatorResult? indicatorResult = await _repository
                    .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

        if (indicatorResult is null)
        {
            return DomainErrors.NotFound<IndicatorResult>();
        }

        IndicatorResult newIndicatorResult = request.Adapt<IndicatorResult>();
        newIndicatorResult.Id = request.Id;

        _repository.Update(entity: newIndicatorResult);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}
