# User Module

Es una clase que hereda de [BaseModule](./../Abstractions/baseModule.md) y contiene las rutas para consumir relacionadas a [User](./../../domain/entities/user.md).

```csharp

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
        app.MapPost("/", async ([FromBody] CreateUserRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateUserCommand command = request.Adapt<CreateUserCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: command, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapGet("/", async ([AsParameters] PaginationQueryParameters parameters, ISender sender, CancellationToken cancellationToken) =>
        {
            string[] ids = GetStringsFromExcludeParameter(exclude: parameters.Exclude);

            GetUsersPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

            ErrorOr<Pagination<User>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return Result<Pagination<User>, Pagination<UserPaginationResponse>>(value: result);
        });

        app.MapGet("/{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
        {
            GetUserByIdQuery query = new(Id: id);

            ErrorOr<User> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<User, UserByIdResponse>(value: result);
        });

        app.MapDelete("/{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
        {
            DeleteUserCommand query = new(id);

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });
    }
}
```
