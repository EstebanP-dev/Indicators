using IndicatorsApi.Application.Features.Roles.GetRoleById;
using IndicatorsApi.Application.Features.Roles.GetRolesPagination;
using IndicatorsApi.Contracts.Features.Roles.GetRoleById;
using IndicatorsApi.Contracts.Features.Roles.GetRolesPagination;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Roles;

/// <summary>
/// Role endpoints.
/// </summary>
public sealed class RoleModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleModule"/> class.
    /// </summary>
    public RoleModule()
        : base("roles")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([FromQuery] int page, [FromQuery] int rows, [FromQuery] string? exclude, ISender sender, CancellationToken cancellationToken) =>
        {
            int[] ids = GetIntsFromExcludeParameter(exclude: exclude);

            GetRolesPaginationQuery query = new(page, rows, ids);

            ErrorOr<Pagination<Role>> result = await sender
                .Send(query, cancellationToken)
                .ConfigureAwait(false);

            return Result<Pagination<Role>, Pagination<RolePaginationResponse>>(result);
        });

        app.MapGet("/{id}", async (int id, ISender sender) =>
        {
            GetRoleByIdQuery query = new(id);

            ErrorOr<Role> result = await sender
                .Send(query, default)
                .ConfigureAwait(true);

            return Result<Role, RoleByIdResponse>(result);
        });
    }
}
