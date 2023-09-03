using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Contracts.Features.Users.GetUserByEmail;

/// <summary>
/// Gets user by email response.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Roles">User roles.</param>
public sealed record class UserByEmailResponse(
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    string Email,
    IEnumerable<Role> Roles);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
