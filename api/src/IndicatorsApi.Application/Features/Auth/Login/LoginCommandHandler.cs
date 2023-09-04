using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Exceptions;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Services;

namespace IndicatorsApi.Application.Features.Auth.Login;

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
    public async Task<ErrorOr<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
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

        return _jwtProvider.GenerateJwt(user);
    }
}
