using IndicatorsApi.Application.Features.Roles.UpdateSection;
using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.UpdateRole;

/// <inheritdoc/>
internal sealed class UpdateRoleCommandHandler
    : ICommandHandler<UpdateRoleCommand, Updated>
{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public UpdateRoleCommandHandler(IRoleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _repository
                    .GetByIdAsync(id: request.Id, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

        if (role is null)
        {
            return DomainErrors.NotFound<Role>();
        }

        role.Name = request.Name;

        _repository.Update(entity: role);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return Result.Updated;
    }
}
