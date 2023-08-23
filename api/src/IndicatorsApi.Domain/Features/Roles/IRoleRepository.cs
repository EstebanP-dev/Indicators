namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Role repository methods.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Gets all roles by pagination.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="rows">Page size.</param>
    /// <param name="totalPages">Total pages.</param>
    /// <returns>Returns a list of response type values.</returns>
    IEnumerable<Role> GetPaginationAsync(int page, int rows, out int totalPages);
}
