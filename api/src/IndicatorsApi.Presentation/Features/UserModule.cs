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
        : base("users")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([AsParameters] PaginationQueryParameters parameters, ISender sender, CancellationToken cancellationToken) =>
        {
            string[] ids = GetStringsFromExcludeParameter(exclude: parameters.Exclude);

            GetUsersPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

            ErrorOr<Pagination<User>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            var test = result.Value.Adapt<Pagination<UserPaginationResponse>>();

            Console.WriteLine(test);

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
                .Send(request: command, cancellationToken: cancellationToken)
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
