namespace IndicatorsApi.Application.Features.Users.Login;

/// <summary>
/// Gets the token for the user.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
public record class LoginCommand(string Email, string Password)
    : ICommand<string>;
