using IndicatorsApi.Domain.Features.ActorTypes;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.ActorTypes;

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
    private readonly IActorTypeRepository _actorTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteActorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="actorTypeRepository">Instance of <see cref="IActorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteActorTypeCommandHandler(IActorTypeRepository actorTypeRepository, IUnitOfWork unitOfWork)
    {
        _actorTypeRepository = actorTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteActorTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ActorType? actorType = await _actorTypeRepository
                .GetByIdAsync(id: ActorTypeId.ToActorTypeId(value: request.Id), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (actorType is null)
            {
                return DomainErrors.NotFound<ActorType>();
            }

            _actorTypeRepository.Delete(entity: actorType);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Deleted;
        }
        catch (DbUpdateException)
        {
            return DomainErrors.CreationOrUpdatingFailed;
        }
        catch (OperationCanceledException)
        {
            return DomainErrors.CancelledOperation;
        }
    }
}