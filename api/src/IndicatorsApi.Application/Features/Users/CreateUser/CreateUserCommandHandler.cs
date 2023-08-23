using System.Net;
using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Application.Features.Users.CreateUser;

/// <inheritdoc/>
internal sealed class CreateUserCommandHandler
    : ICommandHandler<CreateUserCommand>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="passwordHasher">Instance of <see cref="IPasswordHasher"/>.</param>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="unitOfWork">Instance of <see cref="IUnitOfWork"/>.</param>
    public CreateUserCommandHandler(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string passwordHash = _passwordHasher.HashPasword(request.Password, out byte[] salt);

            User user = new()
            {
                Email = request.Email,
                Password = passwordHash,
                Salt = new[] { salt },
            };

            _userRepository.Add(user);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            Either<User, Error> either = await _userRepository
                .GetByEmailAsync(request.Email, cancellationToken)
                .ConfigureAwait(false);

            return either.Match(
                            left: MapSuccessResult,
                            right: MapFailureResult);
        }
        catch (OperationCanceledException ex)
        {
            return Result.Failure(DomainErrors.General.CancelledOperation(ex));
        }
    }

    private static Result MapFailureResult(Error error)
    {
        return Result.Failure(error);
    }

    private static Result MapSuccessResult(User user)
    {
        return Result.Success();
    }
}
