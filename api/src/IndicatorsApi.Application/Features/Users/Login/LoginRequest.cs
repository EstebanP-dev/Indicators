namespace IndicatorsApi.Application.Features.Users.Login;

/// <summary>
/// Sets the user credentials.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record class LoginRequest(string Email, string Password);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
