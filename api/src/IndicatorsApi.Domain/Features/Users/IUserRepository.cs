using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User repository methods.
/// </summary>
public interface IUserRepository
    : IRepository<User>
{
    /// <summary>
    /// Get the user information by email.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns either <see cref="User"/> or <see cref="Error"/> instance.</returns>
    Task<Either<User, Error>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add user roles to the database.
    /// </summary>
    /// <param name="userRoles">List of <see cref="UserRole"/> instances.</param>
    void AddUserRoles(UserRole[] userRoles);

    /// <summary>
    /// Get the users pagination.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="rows">Page size.</param>
    /// <param name="excludes">Exclude users emails.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns either <see cref="Pagination{User}"/> or <see cref="Error"/> instance.</returns>
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
    Task<Either<Pagination<User>, Error>> GetPaginationAsync(int page, int rows, string[]? excludes, CancellationToken cancellationToken = default);
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
}
