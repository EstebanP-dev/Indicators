using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User repository methods.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Get the user information by email.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns either <see cref="User"/> or <see cref="BaseException"/> instance.</returns>
    Task<Either<User, Error>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add user to the database.
    /// </summary>
    /// <param name="user">Instance of <see cref="User"/>.</param>
    void Add(User user);
}
