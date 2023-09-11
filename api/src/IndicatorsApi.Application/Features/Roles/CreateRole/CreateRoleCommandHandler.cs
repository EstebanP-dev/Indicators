using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Roles.CreateRole;

/// <inheritdoc/>
internal sealed class CreateRoleCommandHandler
    : ICommandHandler<CreateRoleCommand, Created>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _roleRepository.Add(entity: request.Adapt<Role>());

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
