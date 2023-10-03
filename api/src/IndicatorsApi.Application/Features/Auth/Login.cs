using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Contracts.Auth;
using IndicatorsApi.Contracts.Users;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Services;

namespace IndicatorsApi.Application.Features.Auth;

/// <summary>
/// Gets the token for the user.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record class LoginCommand(string Email, string Password)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : ICommand<LoginResponse>;

/// <inheritdoc/>
internal sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, LoginResponse>
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
    public async Task<ErrorOr<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository
            .GetByIdAsync(
                id: UserId.ToUserId(request.Email),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return AuthErrors.InvalidCredentials;
        }

        if (user.Salt == null)
        {
            return AuthErrors.RestorePassword;
        }

        bool isValid = _passwordChecker
            .VerifyPassword(
                password: request.Password,
                hash: user.Password,
                salt: user.Salt[0]);

        if (!isValid)
        {
            return AuthErrors.InvalidCredentials;
        }

        return new LoginResponse(Token: _jwtProvider.GenerateJwt(user: user), user.Adapt<UserByIdResponse>());
    }
}