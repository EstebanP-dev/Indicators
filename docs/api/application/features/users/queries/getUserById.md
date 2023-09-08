# Get User By Id

Obetener por id para usuario, se componen de dos elementos. El query que hereda de la interfaz [IQuery](./../../../abstractions/messaging/Iquery.md) y el manejador que hereda de la clase [IQueryHandler](../../../abstractions/messaging/IqueryHandler.md).

```csharp
/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">User id.</param>
public sealed record class GetUserByIdQuery(string Id)
    : IQuery<User>;

```

```csharp
/// <inheritdoc/>
internal sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository
            .GetByIdAsync(
                id: UserId.ToUserId(value: request.Id),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return DomainErrors.NotFound<User>();
        }

        return user;
    }
}
```
