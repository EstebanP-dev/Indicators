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
    /// <returns>Returns either <see cref="User"/> or <see cref="BaseException"/> instance.</returns>
    Task<Either<User, Error>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add user roles to the database.
    /// </summary>
    /// <param name="userRoles">List of <see cref="UserRole"/> instances.</param>
    void AddUserRoles(UserRole[] userRoles);
}
