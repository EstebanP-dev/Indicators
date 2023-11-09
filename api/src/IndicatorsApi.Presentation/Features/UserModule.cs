using IndicatorsApi.Application.Features.Users.CreateUser;
using IndicatorsApi.Application.Features.Users.DeleteUser;
using IndicatorsApi.Application.Features.Users.GetUserById;
using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Contracts.Users;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features.Users;

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
        app
            .MapPost("/", CreateUser)
            .WithTags("Users")
            .WithName(nameof(CreateUser))
            .AllowAnonymous();

        app
            .MapGet("/", GetUsers)
            .WithTags("Users")
            .WithName(nameof(GetUsers));

        app
            .MapGet("/{id}", GetUser)
            .WithTags("Users")
            .WithName(nameof(GetUser));

        app
            .MapDelete("/{id}", DeleteUser)
            .WithTags("Users")
            .WithName(nameof(DeleteUser));
    }

    private static async Task<IResult> CreateUser(
        [FromBody] CreateUserRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateUserCommand command = request.Adapt<CreateUserCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> DeleteUser(
        string id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DeleteUserCommand query = new(id);

        ErrorOr<Deleted> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetUsers(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        string[] ids = GetStringsFromExcludeParameter(exclude: parameters.Exclude);

        GetUsersPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

        ErrorOr<Pagination<User>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result<Pagination<User>, Pagination<UserPaginationResponse>>(value: result);
    }

    private static async Task<IResult> GetUser(string id, ISender sender, CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(Id: id);

        ErrorOr<User> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result<User, UserByIdResponse>(value: result);
    }
}
