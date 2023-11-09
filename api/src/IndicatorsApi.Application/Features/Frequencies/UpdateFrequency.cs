using IndicatorsApi.Application.Abstraction.Messaging;
using IndicatorsApi.Domain.Features.Frequencies;

namespace IndicatorsApi.Application.Features.Frequencies;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Frequency description.</param>
/// <param name="Description">Frequency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateFrequencyCommand(int Id, string Description)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateFrequencyValidator : AbstractValidator<UpdateFrequencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFrequencyValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrequencyRepository"/>.</param>
    public UpdateFrequencyValidator(IFrequencyRepository repository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Id)
            .MustAsync(repository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<Frequency>().Description);

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty();
    }
}

/// <inheritdoc/>
internal sealed class UpdateFrequencyCommandHandler
    : ICommandHandler<UpdateFrequencyCommand, Updated>
{
    private readonly IFrequencyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFrequencyCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrequencyRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateFrequencyCommandHandler(IFrequencyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateFrequencyCommand request, CancellationToken cancellationToken)
    {
        Frequency? frecuency = await _repository
            .GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (frecuency is null)
        {
            return DomainErrors.NotFound<Frequency>();
        }

        frecuency.Description = request.Description;

        _repository.Update(frecuency);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}