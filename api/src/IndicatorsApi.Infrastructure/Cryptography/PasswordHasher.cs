using System.Security.Cryptography;
using System.Text;
using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Domain.Services;

namespace IndicatorsApi.Infrastructure.Cryptography;

/// <summary>
/// Password hasher.
/// </summary>
internal sealed class PasswordHasher
    : IPasswordHasher, IPasswordHashChecker
{
    private readonly int _keySize = 64;
    private readonly int _iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

    /// <inheritdoc/>
    public string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(_keySize);

        byte[] hash = Rfc2898DeriveBytes
            .Pbkdf2(
                password: Encoding.UTF8.GetBytes(password),
                salt: salt,
                iterations: _iterations,
                hashAlgorithm: _hashAlgorithm,
                outputLength: _keySize);

        return Convert.ToHexString(hash);
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        byte[] hashToCompare = Rfc2898DeriveBytes
            .Pbkdf2(
                password: password,
                salt: salt,
                iterations: _iterations,
                hashAlgorithm: _hashAlgorithm,
                outputLength: _keySize);

        return CryptographicOperations
            .FixedTimeEquals(
                left: hashToCompare,
                right: Convert.FromHexString(hash));
    }
}
