using IndicatorsApi.Application.Features.Users.CreateUser;
using IndicatorsApi.Application.Features.Users.GetUserByEmail;
using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Contracts.Features.Users.GetUserByEmail;
using IndicatorsApi.Contracts.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// User endpoints.
/// </summary>
public sealed class UserModule
    : BaseModule
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
        app.MapGet("/", async ([FromQuery] int page, [FromQuery] int rows, [FromQuery] string? exclude, ISender sender, CancellationToken cancellationToken) =>
        {
            GetUsersPaginationQuery query = new(page, rows, (exclude ?? string.Empty).Split(";"));

            ErrorOr<Pagination<User>> result = await sender
                .Send(query, cancellationToken)
                .ConfigureAwait(true);

            return Result<Pagination<User>, Pagination<UserPaginationResponse>>(result);
        });

        app.MapGet("/{email}", async (string email, ISender sender, CancellationToken cancellationToken) =>
        {
            GetUserByEmailQuery query = new(email);

            ErrorOr<User> result = await sender
            .Send(query, cancellationToken)
            .ConfigureAwait(true);

            return Result<User, UserByEmailResponse>(result);
        });

        app.MapPost("/", async ([FromBody] CreateUserRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateUserCommand command = request.Adapt<CreateUserCommand>();

            await sender
                .Send(command, cancellationToken)
                .ConfigureAwait(true);

            return Results.Ok();
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
