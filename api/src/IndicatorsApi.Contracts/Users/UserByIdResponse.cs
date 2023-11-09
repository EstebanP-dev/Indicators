using IndicatorsApi.Contracts.Roles;

namespace IndicatorsApi.Contracts.Users;

/// <summary>
/// Gets user by email response.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Roles">User roles.</param>
public sealed record class UserByIdResponse(
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    string Email,
    IEnumerable<RolePaginationResponse> Roles);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
