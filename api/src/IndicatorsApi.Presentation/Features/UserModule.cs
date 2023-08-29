﻿using IndicatorsApi.Application.Features.Users.CreateUser;
using IndicatorsApi.Application.Features.Users.GetUserByEmail;
using IndicatorsApi.Domain.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// User endpoints.
/// </summary>
public sealed class UserModule
    : CarterModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserModule"/> class.
    /// </summary>
    public UserModule()
        : base($"{Settings.BASEAPI}/users")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{email}", async (string email, ISender sender) =>
        {
            GetUserByEmailQuery query = new(email);

            Result<UserResponse> result = await sender
            .Send(query)
            .ConfigureAwait(true);

            return Results.Ok(result);
        });

        app.MapPost("/", async ([FromBody] UserRequest request, ISender sender) =>
        {
            CreateUserCommand command = request.Adapt<CreateUserCommand>();

            Result result = await sender
                .Send(command)
                .ConfigureAwait(true);

            return Results.Ok(result);
        });
    }
}

/// <summary>
/// Create user endpoint request.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
public sealed record class CreateUserRequest(
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    string Email,
    string Password);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
