using IndicatorsApi.Application.Features.ActorTypes.UpdateSection;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.ActorTypes;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.ActorTypes.UpdateActorType;

/// <inheritdoc/>
internal sealed class UpdateActorTypeCommandHandler
    : ICommandHandler<UpdateActorTypeCommand, Updated>
{
    private readonly IActorTypeRepository _actionTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateActorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="actionTypeRepository">Instance of <see cref="IActorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateActorTypeCommandHandler(IActorTypeRepository actionTypeRepository, IUnitOfWork unitOfWork)
    {
        _actionTypeRepository = actionTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateActorTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ActorType? actionType = await _actionTypeRepository
                    .GetByIdAsync(id: ActorTypeId.ToActorTypeId(value: request.Id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (actionType is null)
            {
                return DomainErrors.NotFound<ActorType>();
            }

            actionType.Name = request.Name;

            _actionTypeRepository.Update(entity: actionType);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Updated;
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
