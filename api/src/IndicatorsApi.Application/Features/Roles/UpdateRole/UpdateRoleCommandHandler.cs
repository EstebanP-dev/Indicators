using IndicatorsApi.Application.Features.Roles.UpdateSection;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Roles.UpdateRole;

/// <inheritdoc/>
internal sealed class UpdateRoleCommandHandler
    : ICommandHandler<UpdateRoleCommand, Updated>
{
    private readonly IRoleRepository _actionTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="actionTypeRepository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateRoleCommandHandler(IRoleRepository actionTypeRepository, IUnitOfWork unitOfWork)
    {
        _actionTypeRepository = actionTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Role? actionType = await _actionTypeRepository
                    .GetByIdAsync(id: RoleId.ToRoleId(value: request.Id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            if (actionType is null)
            {
                return DomainErrors.NotFound<Role>();
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
