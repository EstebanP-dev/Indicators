using IndicatorsApi.Application.Features.Roles.GetRoleById;
using IndicatorsApi.Application.Features.Roles.GetRolesPagination;
using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// Role endpoints.
/// </summary>
public sealed class RoleModule
    : CarterModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleModule"/> class.
    /// </summary>
    public RoleModule()
        : base($"{Settings.BASEAPI}/roles")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([FromQuery] int page, [FromQuery] int rows, [FromQuery] string? exclude, ISender sender, CancellationToken cancellationToken) =>
        {
            string[] excludes = (exclude ?? string.Empty).Split(";");
#pragma warning disable CA1305 // Specify IFormatProvider
            int[] ids = excludes
                .Where(ex => int.TryParse(ex, out var intExclude))
                .Select(ex => int.Parse(ex))
                .ToArray();
#pragma warning restore CA1305 // Specify IFormatProvider

            GetRolesPaginationQuery query = new(page, rows, ids);

            Result<Pagination<UserPaginationResponse>> result = await sender
                .Send(query, cancellationToken)
                .ConfigureAwait(false);

            return Results.Ok(result);
        });

        app.MapGet("/{id}", async (int id, ISender sender) =>
        {
            GetRoleByIdQuery query = new(id);

            Result<RoleResponse> result = await sender
                .Send(query, default)
                .ConfigureAwait(true);

            return Results.Ok(result);
        });
    }
}
