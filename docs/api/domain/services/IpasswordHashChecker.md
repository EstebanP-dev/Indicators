# IPasswordHashChecker

Es un servicio que valida alguna contraseña que se encuentre encriptada con [Salting Hash](https://code-maze.com/csharp-hashing-salting-passwords-best-practices/).

```csharp
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

```
