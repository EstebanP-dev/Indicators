using System.Reflection;

namespace IndicatorsApi.Persistence;

/// <summary>
/// Persistence assembly.
/// </summary>
public static class PersistenceAssembly
{
    /// <summary>
    /// Gets the application assembly.
    /// </summary>
    /// <value>
    /// The application assembly.
    /// </value>
    public static Assembly Assembly => typeof(PersistenceAssembly).Assembly;
}
