using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Application.Features.Roles.GetRolesPagination;

/// <summary>
/// Gets the roles pagination query.
/// </summary>
/// <param name="Page">Page number.</param>
/// <param name="Rows">Page size.</param>
/// <param name="Excludes">Exclude roles ids.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Necessary.")]
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
public sealed record class GetRolesPaginationQuery(int Page, int Rows, int[]? Excludes)
    : IQuery<Pagination<UserPaginationResponse>>;
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
