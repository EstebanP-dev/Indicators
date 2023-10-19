using IndicatorsApi.Domain.Features.Frecuencies;

namespace IndicatorsApi.Application.Features.Frecuencies;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Description">Frecuency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateFrecuencyCommand(string Description)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateFrecuencyValidator : AbstractValidator<CreateFrecuencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFrecuencyValidator"/> class.
    /// </summary>
    public CreateFrecuencyValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(200);
    }
}

/// <inheritdoc/>
internal sealed class CreateFrecuencyCommandHandler
    : ICommandHandler<CreateFrecuencyCommand, Created>
{
    private readonly IFrecuencyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFrecuencyCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrecuencyRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateFrecuencyCommandHandler(IFrecuencyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateFrecuencyCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: new Frecuency
        {
            Id = -1,
            Description = request.Description,
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result.Created;
    }
}