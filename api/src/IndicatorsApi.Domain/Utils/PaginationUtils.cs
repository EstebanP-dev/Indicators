namespace IndicatorsApi.Domain.Utils;

/// <summary>
/// Pagination utils.
/// </summary>
public static class PaginationUtils
{
    /// <summary>
    /// Gets the total pages from pagination query.
    /// </summary>
    /// <param name="totalRows">Total rows from database.</param>
    /// <param name="pageSize">Page size from query string.</param>
    /// <returns>Returns the total pages base on query parameters and database rows.</returns>
    public static int ConvertTotalPages(int totalRows, int pageSize)
    {
        double diference = Convert.ToDouble(totalRows) / Convert.ToDouble(pageSize);

        return (int)Math.Ceiling(diference);
    }
}
