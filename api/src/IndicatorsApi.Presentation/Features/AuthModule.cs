﻿using IndicatorsApi.Application.Features.Users.Login;
using IndicatorsApi.Domain.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// Auth endpoints.
/// </summary>
public class AuthModule
    : CarterModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthModule"/> class.
    /// </summary>
    public AuthModule()
        : base($"{Settings.BASEAPI}/auth")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async ([FromBody] LoginRequest request, ISender sender) =>
        {
            LoginCommand command = request.Adapt<LoginCommand>();

            Result<string> result = await sender.Send(command).ConfigureAwait(true);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        });
    }
}
