using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Role repository methods.
/// </summary>
public interface IRoleRepository
    : IRepository<Role>
{
    /// <summary>
    /// Gets all roles by pagination.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="rows">Page size.</param>
    /// <param name="totalPages">Total pages.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns a list of response type values.</returns>
    Task<Either<IEnumerable<Role>, Error>> GetPaginationAsync(int page, int rows, out int totalPages, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a role by id.
    /// </summary>
    /// <param name="id">Role id.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns either an instance of response type or result error.</returns>
    Task<Either<Role, Error>> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Get bulk roles by ids.
    /// </summary>
    /// <param name="ids">Role ids.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns either a list of response type instances or result error.</returns>
    Task<Either<IEnumerable<Role>, Error>> GetBulkByIdsAsync(int[] ids, CancellationToken cancellationToken);

    /// <summary>
    /// Gets roles by user id.
    /// </summary>
    /// <param name="userId">User email.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns either a list of response type ids' instances or result error.</returns>
    Task<Either<IEnumerable<int>, Error>> GetRolesIdsByUserIdAsync(string userId, CancellationToken cancellationToken);
}
