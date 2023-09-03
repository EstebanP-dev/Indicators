using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.GetRoleById;

/// <summary>
/// Get Role By Id Query.
/// </summary>
/// <param name="Id">Role id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetRoleByIdQuery(int Id)
    : IQuery<Role>;
