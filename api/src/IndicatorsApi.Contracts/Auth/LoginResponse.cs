using IndicatorsApi.Contracts.Features.Roles.GetRolesPagination;
using IndicatorsApi.Contracts.Features.Users.GetUserByEmail;
using IndicatorsApi.Contracts.Features.Users.GetUsersPagination;

namespace IndicatorsApi.Contracts.Auth;

/// <summary>
/// Login response.
/// </summary>
/// <param name="Token">Api token.</param>
/// <param name="User">User info.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class LoginResponse(string Token, UserByIdResponse User);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
