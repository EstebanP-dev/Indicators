using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">ActorType id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteActorTypeCommand(int Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class DeleteActorTypeCommandHandler
    : ICommandHandler<DeleteActorTypeCommand, Deleted>
{
    private readonly IActorTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteActorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteActorTypeCommandHandler(IActorTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteActorTypeCommand request, CancellationToken cancellationToken)
    {
        ActorType? actorType = await _repository
            .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (actorType is null)
        {
            return DomainErrors.NotFound<ActorType>();
        }

        _repository.Delete(entity: actorType);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Deleted;
    }
}