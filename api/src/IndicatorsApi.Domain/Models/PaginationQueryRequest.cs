namespace IndicatorsApi.Domain.Models;

/// <summary>
/// Pagination request.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class PaginationQueryRequest(int Page, int Rows, string? Exclude);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter