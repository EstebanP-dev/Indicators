using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.GetRoleById;

/// <inheritdoc/>
internal sealed class GetRoleByIdQueryHandler
    : IQueryHandler<GetRoleByIdQuery, Role>
{
    private readonly IRoleRepository _actorTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRoleByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="actorTypeRepository">Instance of <see cref="IRoleRepository"/>.</param>
    public GetRoleByIdQueryHandler(IRoleRepository actorTypeRepository)
    {
        _actorTypeRepository = actorTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Role? actorType = await _actorTypeRepository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (actorType is null)
        {
            return DomainErrors.NotFound<Role>();
        }

        return actorType;
    }
}
