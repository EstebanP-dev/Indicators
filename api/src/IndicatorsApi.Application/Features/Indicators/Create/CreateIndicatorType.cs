using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.Create;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorTypeCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateIndicatorTypeValidator : AbstractValidator<CreateIndicatorTypeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIndicatorTypeValidator"/> class.
    /// </summary>
    public CreateIndicatorTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(200);
    }
}

/// <inheritdoc/>
internal sealed class CreateIndicatorTypeCommandHandler
    : ICommandHandler<CreateIndicatorTypeCommand, Created>
{
    private readonly IIndicatorTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIndicatorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateIndicatorTypeCommandHandler(IIndicatorTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateIndicatorTypeCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: request.Adapt<IndicatorType>());

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Created;
    }
}