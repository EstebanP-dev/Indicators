using System.Reflection;

namespace IndicatorsApi.Infrastructure;

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
