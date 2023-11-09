# Domain Errors

Es una clase estatica con propiedades, tambien estaticas, que retornan la clase **Error** de la libreria **[ErrorOr](https://github.com/amantinband/error-or)**. El sentido de estas es tener unos textos estandarizados para cada posible error que se presente en nuestro código. Estos errores únicamente deben implementarse en la capa de [Aplicación](#application).

```csharp
/// <summary>
/// Domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Gets the undefined error.
    /// </summary>
    /// <value>
    /// The undefined error.
    /// </value>
    public static Error UndefinedError => Error.Unexpected(
            description: "An error occurred while processing the request. Try to contact the support team.");
    
    /// <summary>
    /// Gets the creation or updation failed error.
    /// </summary>
    /// <value>
    /// The creation or updation failed error.
    /// </value>
    public static Error CreationOrUpdatingFailed => Error.Failure(
        description: "Something was wrong. Try again later.");
}
```
