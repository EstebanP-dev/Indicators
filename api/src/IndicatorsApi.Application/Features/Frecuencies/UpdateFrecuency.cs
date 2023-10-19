using IndicatorsApi.Application.Abstraction.Messaging;
using IndicatorsApi.Domain.Features.Frecuencies;

namespace IndicatorsApi.Application.Features.Frecuencies;

/// <summary>
/// Update command.
/// </summary>
/// <param name="Id">Frecuency description.</param>
/// <param name="Description">Frecuency description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateFrecuencyCommand(int Id, string Description)
    : IUpdateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class UpdateFrecuencyValidator : AbstractValidator<UpdateFrecuencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFrecuencyValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrecuencyRepository"/>.</param>
    public UpdateFrecuencyValidator(IFrecuencyRepository repository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Id)
            .MustAsync(repository.DoEntityExistsAsync)
            .WithMessage(DomainErrors.NotFound<Frecuency>().Description);

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty();
    }
}

/// <inheritdoc/>
internal sealed class UpdateFrecuencyCommandHandler
    : ICommandHandler<UpdateFrecuencyCommand, Updated>
{
    private readonly IFrecuencyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFrecuencyCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrecuencyRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateFrecuencyCommandHandler(IFrecuencyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateFrecuencyCommand request, CancellationToken cancellationToken)
    {
        Frecuency? frecuency = await _repository
            .GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (frecuency is null)
        {
            return DomainErrors.NotFound<Frecuency>();
        }

        frecuency.Description = request.Description;

        _repository.Update(frecuency);

        await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}