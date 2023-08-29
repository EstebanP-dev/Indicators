using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.GetRoleById;

/// <inheritdoc/>
internal sealed class GetRoleByIdQueryHandler
    : IQueryHandler<GetRoleByIdQuery, RoleResponse>
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRoleByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">Instance of <see cref="IRoleRepository"/>.</param>
    public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Either<Role, Error> roleResponse = await _roleRepository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return roleResponse
                .Match(MapRoleResponse, MapErrorResponse);
    }

    private Result<RoleResponse> MapErrorResponse(Error error)
    {
        return Result.Failure<RoleResponse>(error);
    }

    private Result<RoleResponse> MapRoleResponse(Role role)
    {
        return role.Adapt<RoleResponse>();
    }
}
