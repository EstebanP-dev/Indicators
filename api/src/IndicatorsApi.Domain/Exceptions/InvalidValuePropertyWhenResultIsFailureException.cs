using System.Runtime.CompilerServices;

namespace IndicatorsApi.Domain.Exceptions;

/// <summary>
/// Exception that is thrown when the value of a failure result is accessed.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class InvalidValuePropertyWhenResultIsFailureException : BaseException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidValuePropertyWhenResultIsFailureException"/> class.
    /// </summary>
    /// <param name="propertyName">Object where is instanced the result intance method.</param>
    /// <param name="filePath">Route of the instanced object.</param>
    /// <param name="fileNumber">Number of the line of code.</param>
    public InvalidValuePropertyWhenResultIsFailureException([CallerMemberName] string propertyName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int fileNumber = 0)
        : base("The value of a failure result can not be accessed.", null)
    {
    }
}
