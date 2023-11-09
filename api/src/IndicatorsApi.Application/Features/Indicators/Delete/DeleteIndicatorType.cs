using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.Delete;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteIndicatorTypeCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class DeleteIndicatorTypeValidator : AbstractValidator<DeleteIndicatorTypeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIndicatorTypeValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    public DeleteIndicatorTypeValidator(IIndicatorTypeRepository repository)
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
            .WithMessage(DomainErrors.NotFound<IndicatorType>().Description);
    }
}

/// <inheritdoc/>
internal sealed class DeleteIndicatorTypeCommandHandler
    : ICommandHandler<DeleteIndicatorTypeCommand, Deleted>
{
    private readonly IIndicatorTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIndicatorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteIndicatorTypeCommandHandler(IIndicatorTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteIndicatorTypeCommand request, CancellationToken cancellationToken)
    {
        IndicatorType? indicatorType = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<IndicatorType>();
        }

        _repository.Delete(entity: indicatorType);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Deleted;
    }
}