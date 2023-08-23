namespace IndicatorsApi.Application.Abstraction;

/// <summary>
/// Represents the password hasher interface.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the specified password.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <param name="salt">Password salt.</param>
    /// <returns>The password hash.</returns>
    string HashPasword(string password, out byte[] salt);
}
