using IndicatorsApi.Application.Features.Roles.CreateRole;
using IndicatorsApi.Application.Features.Roles.DeleteRole;
using IndicatorsApi.Application.Features.Roles.GetRoleById;
using IndicatorsApi.Application.Features.Roles.GetRoles;
using IndicatorsApi.Application.Features.Roles.GetRolesPagination;
using IndicatorsApi.Application.Features.Roles.UpdateSection;
using IndicatorsApi.Contracts.Roles;
using IndicatorsApi.Domain.Errors;
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
        app
            .MapPost("/", CreateRole)
            .WithTags("Roles")
            .WithName(nameof(CreateRole));

        app
            .MapPut("/{id}", UpdateRole)
            .WithTags("Roles")
            .WithName(nameof(UpdateRole));

        app
            .MapDelete("/{id}", DeleteRole)
            .WithTags("Roles")
            .WithName(nameof(DeleteRole));

        app
            .MapGet("/", GetRolesPagination)
            .WithTags("Roles")
            .WithName(nameof(GetRolesPagination));

        app
            .MapGet("/all", GetRoles)
            .WithTags("Roles")
            .WithName(nameof(GetRoles));

        app
            .MapGet("/{id}", GetRole)
            .WithTags("Roles")
            .WithName(nameof(GetRole));
    }

    private static async Task<IResult> CreateRole(
        [FromBody] CreateRoleRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateRoleCommand command = request.Adapt<CreateRoleCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> UpdateRole(
        int id,
        [FromBody] UpdateRoleRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
        }

        UpdateRoleCommand query = request.Adapt<UpdateRoleCommand>();

        ErrorOr<Updated> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteRole(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteRoleCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetRolesPagination(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

        GetRolesPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<Role>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<Role>, Pagination<RolePaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetRoles(
        [FromQuery] string? exclude,
        ISender sender,
        CancellationToken cancellationToken)
    {
        int[] ids = GetIntsFromExcludeParameter(exclude: exclude);

        GetRolesQuery query = new(Excludes: ids);

        ErrorOr<IEnumerable<Role>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<IEnumerable<Role>, IEnumerable<RolePaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetRole(int id, ISender sender, CancellationToken cancellationToken)
    {
        GetRoleByIdQuery query = new(Id: id);

        ErrorOr<Role> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<Role, RoleByIdResponse>(value: result);
    }
}
