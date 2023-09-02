using IndicatorsApi.Application.Features.Users.GetUserByEmail;

namespace IndicatorsApi.Application.Features.Users.GetUsersPagination;

/// <summary>
/// Gets user pagination.
/// </summary>
/// <param name="Page">Page number.</param>
/// <param name="Rows">Page size.</param>
/// <param name="Excludes">Exclude users emails.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
#pragma warning disable CA1819 // Properties should not return arrays
public sealed record class GetUsersPaginationQuery(int Page, int Rows, string[]? Excludes)
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
    : IQuery<Pagination<UserPaginationResponse>>;
