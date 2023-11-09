# Application Assembly

Es una clase estatica que nos permite acceder a la clase ensambladora del proyecto [Infrastructure](infrastructure.md).

```csharp
/// <summary>
/// Infrastructure assembly.
/// </summary>
public static class InfrastructureAssembly
{
    /// <summary>
    /// Gets the application assembly.
    /// </summary>
    /// <value>
    /// The application assembly.
    /// </value>
    public static Assembly Assembly => typeof(InfrastructureAssembly).Assembly;
}

```
