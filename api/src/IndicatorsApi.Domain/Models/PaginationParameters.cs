namespace IndicatorsApi.Domain.Models;

/// <summary>
/// Pagination parameters.
/// </summary>
/// <typeparam name="TKey">Model id type.</typeparam>
/// <param name="Page">Page number.</param>
/// <param name="Rows">Page size.</param>
/// <param name="Excludes">Exclude ids.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
#pragma warning disable CA1819 // Properties should not return arrays
public sealed record class PaginationParameters<TKey>(int Page, int Rows, TKey[]? Excludes)
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    where TKey : class;
