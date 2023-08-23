using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain.Exceptions;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Services;

namespace IndicatorsApi.Application.Features.Users.Login;

/// <inheritdoc/>
internal sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHashChecker _passwordChecker;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Instance of <see cref="IUserRepository"/>.</param>
    /// <param name="jwtProvider">Instance of <see cref="IJwtProvider"/>.</param>
    /// <param name="passwordChecker">Instance of <see cref="IPasswordHashChecker"/>.</param>
    public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHashChecker passwordChecker)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordChecker = passwordChecker;
    }

    /// <inheritdoc/>
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Either<User, Error> user = await _userRepository
            .GetByEmailAsync(request.Email, cancellationToken)
            .ConfigureAwait(false);

        return user
            .Match(
                left: left => ComparePasswords(left, request.Password),
                right: ErrorHandler);
    }

    private static Result<string> ErrorHandler(Error error)
    {
        if (error.Exception is UserByEmailCannotBeFoundException)
        {
            return Result.Failure<string>(DomainErrors.Auth.InvalidCredentials());
        }

        return Result.Failure<string>(error);
    }

    private Result<string> ComparePasswords(User user, string password)
    {
        if (user.Salt is null)
        {
            return Result.Failure<string>(DomainErrors.Auth.NullPasswordSalt(user.Email));
        }

        bool isValid = _passwordChecker
            .VerifyPassword(password, user.Password, user.Salt[0]);

        if (!isValid)
        {
            return Result.Failure<string>(DomainErrors.Auth.InvalidCredentials());
        }

        return GenerateJwt(user);
    }

    private Result<string> GenerateJwt(User user)
    {
        return _jwtProvider.GenerateJwt(user);
    }
}
