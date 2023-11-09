using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.Delete;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">Indicator id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteIndicatorCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class DeleteIndicatorValidator : AbstractValidator<DeleteIndicatorCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIndicatorValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    public DeleteIndicatorValidator(IIndicatorRepository repository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Id)
            .MustAsync(repository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<Indicator>().Description);
    }
}

/// <inheritdoc/>
internal sealed class DeleteIndicatorCommandHandler
    : ICommandHandler<DeleteIndicatorCommand, Deleted>
{
    private readonly IIndicatorRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIndicatorCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteIndicatorCommandHandler(IIndicatorRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteIndicatorCommand request, CancellationToken cancellationToken)
    {
        Indicator? indicatorType = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<Indicator>();
        }

        _repository.Delete(entity: indicatorType);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Deleted;
    }
}