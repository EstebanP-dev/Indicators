# Create User

La creación del usuario se compone de dos cosas. El comando que hereda de [ICommand](./../../../abstractions/messaging/Icommand.md) y el manejador de commando que hereda de [ICommandHandler](./../../../abstractions/messaging/IcommandHandler.md).

## Command

Es una clase que funciona como modelo request. Esta clase únicamente contiene los datos que esperamos en el manejador o al realizar la lógica de negocio.

```csharp
/// <summary>
/// Create command.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
public sealed record class CreateUserCommand(string Email, string Password)
    : ICommand;letter
```

## Command Handler

En esta clase se encuentra toda la lógica.

```csharp
internal sealed class CreateUserCommandHandler
    : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="passwordHasher">Instance of <see cref="IPasswordHasher"/>.</param>
    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository
                .GetByIdAsync(id: UserId.ToUserId(value: request.Email), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (user is not null)
            {
                return DomainErrors.AlreadyExists<User>();
            }

            string passwordHash = _passwordHasher.HashPasword(password: request.Password, salt: out byte[] salt);

            user = new(id: UserId.ToUserId(value: request.Email), password: passwordHash)
            {
                Salt = new[] { salt },
            };

            _userRepository.Add(entity: user);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return Result.Success;
        }
        catch (DbUpdateException)
        {
            return DomainErrors.CreationOrUpdatingFailed;
        }
        catch (OperationCanceledException)
        {
            return DomainErrors.CancelledOperation;
        }
    }
}
```
