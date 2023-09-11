using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.ActorTypes;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.ActorTypes.CreateActorType;

/// <inheritdoc/>
internal sealed class CreateActorTypeCommandHandler
    : ICommandHandler<CreateActorTypeCommand, Created>
{
    private readonly IActorTypeRepository _actorTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateActorTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="actorTypeRepository">Instance of <see cref="IActorTypeRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateActorTypeCommandHandler(IActorTypeRepository actorTypeRepository, IUnitOfWork unitOfWork)
    {
        _actorTypeRepository = actorTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateActorTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _actorTypeRepository.Add(entity: request.Adapt<ActorType>());

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Created;
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
