# IJwtProvider

Es una interfaz o contrato para la creación un Json Web Token.

```csharp
/// <summary>
/// Json Web Token provider.
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    ///   Generates a Json Web Token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <returns>Json Web Token.</returns>
    string GenerateJwt(User user);
}
```
