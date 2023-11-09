using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Application.Features.Roles.GetRoles;

/// <summary>
/// Gets the pagination query.
/// </summary>
/// <param name="Excludes">Exclude roles ids.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Necessary.")]
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
public sealed record class GetRolesQuery(int[]? Excludes)
    : IQuery<IEnumerable<Role>>;
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
