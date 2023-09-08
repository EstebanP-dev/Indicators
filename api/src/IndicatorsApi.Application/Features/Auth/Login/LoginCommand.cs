using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Auth.Login;

/// <summary>
/// Gets the token for the user.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record class LoginCommand(string Email, string Password)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : ICommand<(string, User)>;
