using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Id">Actor id.</param>
/// <param name="Name">Actor name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateActorCommand(string Id, string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateActorValidator : AbstractValidator<CreateActorCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateActorValidator"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorRepository"/>.</param>
    public CreateActorValidator(IActorRepository repository)
    {
        Func<string, CancellationToken, Task<bool>> validateIdUnique =
                async (id, cancellationToken) =>
                    !(await repository
                            .DoEntityExistsAsync(id, cancellationToken)
                            .ConfigureAwait(false));

        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Id)
            .MustAsync(validateIdUnique)
            .WithMessage(DomainErrors.AlreadyExists<Actor>().Description);

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(200);
    }
}

/// <inheritdoc/>
internal sealed class CreateActorCommandHandler
    : ICommandHandler<CreateActorCommand, Created>
{
    private readonly IActorRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateActorCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateActorCommandHandler(IActorRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: request.Adapt<Actor>());

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Created;
    }
}