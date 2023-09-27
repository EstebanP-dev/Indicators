﻿using IndicatorsApi.Application.Features.Auth.Login;
using IndicatorsApi.Contracts.Auth;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Presentation.Features.Auth;

/// <summary>
/// Auth endpoints.
/// </summary>
public sealed class AuthModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthModule"/> class.
    /// </summary>
    public AuthModule()
        : base("auth")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/login", Login)
            .WithTags("Authentication")
            .WithName(nameof(Login))
            .AllowAnonymous();
    }

    private static async Task<IResult> Login([FromBody] LoginRequest request, ISender sender)
    {
        LoginCommand command = request.Adapt<LoginCommand>();

        ErrorOr<(string, User)> result = await sender.Send(command)
            .ConfigureAwait(true);

        return Result<(string, User), LoginResponse>(result);
    }
}
