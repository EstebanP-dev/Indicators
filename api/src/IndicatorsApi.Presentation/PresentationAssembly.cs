using System.Reflection;

namespace IndicatorsApi.Presentation;

/// <summary>
/// Presentation assembly.
/// </summary>
public static class PresentationAssembly
{
    /// <summary>
    /// Gets the presentation assembly.
    /// </summary>
    /// <value>
    /// The presentation assembly.
    /// </value>
    public static Assembly Assembly => typeof(PresentationAssembly).Assembly;
}
