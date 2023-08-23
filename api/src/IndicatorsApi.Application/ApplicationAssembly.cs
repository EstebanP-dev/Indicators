using System.Reflection;

namespace IndicatorsApi.Application;

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
