using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.Delete;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">IndicatorResult id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteIndicatorResultCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class DeleteIndicatorResultValidator : AbstractValidator<DeleteIndicatorResultCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIndicatorResultValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    public DeleteIndicatorResultValidator(IIndicatorResultRepository repository)
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
    }
}

/// <inheritdoc/>
internal sealed class DeleteIndicatorResultCommandHandler
    : ICommandHandler<DeleteIndicatorResultCommand, Deleted>
{
    private readonly IIndicatorResultRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIndicatorResultCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteIndicatorResultCommandHandler(IIndicatorResultRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteIndicatorResultCommand request, CancellationToken cancellationToken)
    {
        IndicatorResult? indicatorType = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<IndicatorResult>();
        }

        _repository.Delete(entity: indicatorType);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Deleted;
    }
}