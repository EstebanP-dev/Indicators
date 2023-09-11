namespace IndicatorsApi.Contracts.Roles;

/// <summary>
/// Gets role by id response.
/// </summary>
/// <param name="Id">Role id.</param>
/// <param name="Name">Role name.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class RoleByIdResponse(int Id, string Name);
