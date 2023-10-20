using IndicatorsApi.Domain.Features.Frequencies;

namespace IndicatorsApi.Application.Features.Frequencies;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Description">Frequency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateFrequencyCommand(string Description)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateFrequencyValidator : AbstractValidator<CreateFrequencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFrequencyValidator"/> class.
    /// </summary>
    public CreateFrequencyValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(200);
    }
}

/// <inheritdoc/>
internal sealed class CreateFrequencyCommandHandler
    : ICommandHandler<CreateFrequencyCommand, Created>
{
    private readonly IFrequencyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFrequencyCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrequencyRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateFrequencyCommandHandler(IFrequencyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateFrequencyCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: new Frequency
        {
            Id = -1,
            Description = request.Description,
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result.Created;
    }
}