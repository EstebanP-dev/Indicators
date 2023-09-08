# JwtOptions

Esta clase es un modelo que contiene los parámetros para generar el json web token.

```csharp
/// <summary>
/// Map the json web token options.
/// </summary>
public class JwtOptions
{
    /// <summary>
    ///  Gets the issuer that will be used to sign the token.
    /// </summary>
    /// <value>
    /// The issuer that will be used to sign the token.
    /// </value>
    public string? Issuer { get; init; }

    /// <summary>
    /// Gets the audience that will be used to sign the token.
    /// </summary>
    /// <value>
    /// The audience that will be used to sign the token.
    /// </value>
    public string? Audience { get; init; }

    /// <summary>
    /// Gets the secret key that will be used to sign the token.
    /// </summary>
    /// <value>
    /// The secret key that will be used to sign the token.
    /// </value>
    public string? SecretKey { get; init; }

    /// <summary>
    /// Gets the expiration time in minutes.
    /// </summary>
    /// <value>
    /// The expiration time in minutes.
    /// </value>
    public int ExpirationTime { get; init; }
}

```
