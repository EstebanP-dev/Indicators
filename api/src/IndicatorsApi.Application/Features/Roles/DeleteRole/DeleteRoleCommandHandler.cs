using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Roles.DeleteRole;

/// <inheritdoc/>
internal sealed class DeleteRoleCommandHandler
    : ICommandHandler<DeleteRoleCommand, Deleted>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Role? role = await _roleRepository
                .GetByIdAsync(id: RoleId.ToRoleId(value: request.Id), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (role is null)
            {
                return DomainErrors.NotFound<Role>();
            }

            _roleRepository.Delete(entity: role);

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
