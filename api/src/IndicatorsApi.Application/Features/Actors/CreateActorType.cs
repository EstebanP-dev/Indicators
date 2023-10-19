using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Create command.
/// </summary>
/// <param name="Name">ActorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateActorTypeCommand(string Name)
    : ICreateCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class CreateActorTypeCommandHandler
    : ICommandHandler<CreateActorTypeCommand, Created>
{
    private readonly IActorTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateActorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateActorTypeCommandHandler(IActorTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateActorTypeCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(entity: request.Adapt<ActorType>());

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Created;
    }
}