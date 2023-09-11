namespace IndicatorsApi.Contracts.Users;

/// <summary>
/// Gets users pagination response.
/// </summary>
/// <param name="Email">User email.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class UserPaginationResponse(string Email);
