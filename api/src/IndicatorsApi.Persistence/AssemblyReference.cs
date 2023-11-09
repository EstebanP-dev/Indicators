using System.Reflection;

namespace IndicatorsApi.Persistence;

/// <summary>
/// Reference of persistence project assembly.
/// </summary>
public static class AssemblyReference
{
    private static readonly Assembly _assembly = typeof(AssemblyReference).Assembly;

    /// <summary>
    /// Gets assembly instance.
    /// </summary>
    /// <value>
    /// Assembly instance.
    /// </value>
    public static Assembly Assembly { get => _assembly; }
}
