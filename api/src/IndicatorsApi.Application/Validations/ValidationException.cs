using IndicatorsApi.Domain.Exceptions;

namespace IndicatorsApi.Application.Validations;

/// <summary>
/// This is a custom exception that is thrown when a validation fails.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class ValidationException : BaseException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="errors">Validation errors.</param>
    public ValidationException(IEnumerable<Error> errors)
        : base("Validation failed.")
    {
        Errors = errors;
    }

    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    /// <value>
    /// The validation errors.
    /// </value>
    public IEnumerable<Error> Errors { get; }
}
