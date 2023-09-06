namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// Pagination parameters.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class PaginationQueryParameters(int Page, int Rows, string? Exclude);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter