namespace IndicatorsApi.Domain.Services;

/// <summary>
/// Represents the password hash checker interfaces.
/// </summary>
public interface IPasswordHashChecker
{
    /// <summary>
    /// Verify the password.
    /// </summary>
    /// <param name="password">User password.</param>
    /// <param name="hash">User hash password.</param>
    /// <param name="salt">User salt.</param>
    /// <returns>If the password is correct.</returns>
    bool VerifyPassword(string password, string hash, byte[] salt);
}
