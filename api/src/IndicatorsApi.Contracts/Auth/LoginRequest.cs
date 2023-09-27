namespace IndicatorsApi.Contracts.Auth;

/// <summary>
/// Sets the user credentials.
/// </summary>
/// <param name="Username">User email.</param>
/// <param name="Password">User password.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record class LoginRequest(string Username, string Password);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
