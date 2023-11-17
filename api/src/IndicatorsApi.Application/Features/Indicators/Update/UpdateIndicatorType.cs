using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Indicators.Update;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateIndicatorTypeCommand(int Id, string Name)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateIndicatorTypeValidator : AbstractValidator<UpdateIndicatorTypeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorTypeValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    public UpdateIndicatorTypeValidator(IIndicatorTypeRepository repository)
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

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(200);
    }
}

/// <inheritdoc/>
internal sealed class UpdateIndicatorTypeCommandHandler
    : ICommandHandler<UpdateIndicatorTypeCommand, Updated>
{
    private readonly IIndicatorTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIndicatorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateIndicatorTypeCommandHandler(IIndicatorTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateIndicatorTypeCommand request, CancellationToken cancellationToken)
    {
        IndicatorType? indicatorType = await _repository
                    .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

        if (indicatorType is null)
        {
            return DomainErrors.NotFound<IndicatorType>();
        }

        indicatorType.Name = request.Name;

        _repository.Update(entity: indicatorType);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}
