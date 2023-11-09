using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Features.Roles.DeleteRole;

/// <inheritdoc/>
internal sealed class DeleteRoleCommandHandler
    : ICommandHandler<DeleteRoleCommand, Deleted>
{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public DeleteRoleCommandHandler(IRoleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Deleted>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _repository
                .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        if (role is null)
        {
            return DomainErrors.NotFound<Role>();
        }

        _repository.Delete(entity: role);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Deleted;
    }
}
