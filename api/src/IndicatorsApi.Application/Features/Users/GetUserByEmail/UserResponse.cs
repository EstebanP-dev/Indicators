namespace IndicatorsApi.Application.Features.Users.GetUserByEmail;

/// <summary>
/// Gets user by email response.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
public sealed record class UserResponse(
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    string Email,
    string Password);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
