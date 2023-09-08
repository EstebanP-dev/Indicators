# Application Assembly

Es una clase estatica que nos permite acceder a la clase ensambladora del proyecto [Application](./application.md).

```csharp
/// <summary>
/// Application assembly.
/// </summary>
public static class ApplicationAssembly
{
    /// <summary>
    /// Gets the application assembly.
    /// </summary>
    /// <value>
    /// The application assembly.
    /// </value>
    public static Assembly Assembly => typeof(ApplicationAssembly).Assembly;
}
```
