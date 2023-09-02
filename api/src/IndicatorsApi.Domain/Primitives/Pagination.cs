namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// Pagination response object.
/// </summary>
/// <typeparam name="TResponse">Entity or response model.</typeparam>
public class Pagination<TResponse>
    where TResponse : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pagination{TResponse}"/> class.
    /// </summary>
    /// <param name="totalPages">Total pages.</param>
    /// <param name="currentPage">Current page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="response">Result.</param>
    public Pagination(int totalPages, int currentPage, int pageSize, IEnumerable<TResponse> response)
    {
        TotalPages = totalPages;
        CurrentPage = currentPage;
        PageSize = pageSize;
        Response = response ?? Enumerable.Empty<TResponse>();
    }

    /// <summary>
    /// Gets total pages.
    /// </summary>
    /// <value>
    /// Total pages.
    /// </value>
    public int TotalPages { get; init; }

    /// <summary>
    /// Gets current page.
    /// </summary>
    /// <value>
    /// Current page.
    /// </value>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Gets page size.
    /// </summary>
    /// <value>
    /// Page size.
    /// </value>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets the response list.
    /// </summary>
    /// <value>
    /// The response list.
    /// </value>
    public IEnumerable<TResponse> Response { get; init; }
}
