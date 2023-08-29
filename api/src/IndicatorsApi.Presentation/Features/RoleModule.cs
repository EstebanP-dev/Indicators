using IndicatorsApi.Application.Features.Roles.GetRoleById;
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
