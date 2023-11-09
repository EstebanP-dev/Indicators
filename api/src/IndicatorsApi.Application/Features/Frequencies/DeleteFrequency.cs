using IndicatorsApi.Domain.Features.Frequencies;

namespace IndicatorsApi.Application.Features.Frequencies;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">Frequency id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record DeleteFrequencyCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class DeleteFrequencyValidator : AbstractValidator<DeleteFrequencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFrequencyValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrequencyRepository"/>.</param>
    public DeleteFrequencyValidator(IFrequencyRepository repository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Id)
            .MustAsync(repository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<Frequency>().Description);
    }
}

/// <inheritdoc/>
internal sealed class DeleteFrequencyCommandHandler
    : ICommandHandler<DeleteFrequencyCommand, Deleted>
{
    private readonly IFrequencyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFrequencyCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrequencyRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteFrequencyCommandHandler(IFrequencyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteFrequencyCommand request, CancellationToken cancellationToken)
    {
        Frequency? frequency = await _repository
            .GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (frequency is null)
        {
            return DomainErrors.NotFound<Frequency>();
        }

        _repository.Delete(frequency);

        await _unitOfWork
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Deleted;
    }
}