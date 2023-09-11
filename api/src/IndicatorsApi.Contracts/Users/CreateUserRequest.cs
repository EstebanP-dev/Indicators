namespace IndicatorsApi.Contracts.Users;

/// <summary>
/// Create User Request.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="Password">User password.</param>
/// <param name="Roles">User roles.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Not necessary.")]
public record class CreateUserRequest(string Email, string Password, IEnumerable<int> Roles);
