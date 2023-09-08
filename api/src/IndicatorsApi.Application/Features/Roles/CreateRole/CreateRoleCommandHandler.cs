using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Roles.CreateRole;

/// <inheritdoc/>
internal sealed class CreateRoleCommandHandler
    : ICommandHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _actorTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="actorTypeRepository">Instance of <see cref="IRoleRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateRoleCommandHandler(IRoleRepository actorTypeRepository, IUnitOfWork unitOfWork)
    {
        _actorTypeRepository = actorTypeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _actorTypeRepository.Add(entity: request.Adapt<Role>());

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Success;
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
