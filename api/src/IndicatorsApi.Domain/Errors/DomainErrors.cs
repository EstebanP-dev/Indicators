using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Errors;

#pragma warning disable CA1308 // Normalize strings to uppercase
#pragma warning disable SA1201

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
    /// Gets the cancelled operation error.
    /// </summary>
    /// <value>
    /// The cancelled operation error.
    /// </value>
    public static Error CancelledOperation => Error.Unexpected(
        code: "General.CancelledOperation",
        description: "The operation was cancelled.");

    /// <summary>
    /// Gets the not found error.
    /// </summary>
    /// <typeparam name="T">Class type.</typeparam>
    /// <returns>Returns the not found error.</returns>
    public static Error NotFound<T>()
        where T : class
        => Error.NotFound(
        description: $"The {typeof(T).Name.ToLowerInvariant()} was not found.");

    /// <summary>
    /// Gets the bulk not found error.
    /// </summary>
    /// <value>
    /// The bulk not found error.
    /// </value>
    public static Error BulkNotFound => Error.Unexpected(
        code: "General.BulkNotFound",
        description: "Something was wrong. Try again later.");

    /// <summary>
    /// Gets the already exists error.
    /// </summary>
    /// <typeparam name="T">Class type.</typeparam>
    /// <returns>Returns the already exists error.</returns>
    public static Error AlreadyExists<T>()
        where T : class
        => Error.Conflict(
        code: "General.AlreadyExists",
        description: $"The {typeof(T).Name.ToLowerInvariant()} already exists.");

    /// <summary>
    /// Gets the no coicidence error.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    /// <param name="left">Left value.</param>
    /// <param name="right">Right value.</param>
    /// <returns>Returns a no coincidence error.</returns>
    public static Error NoCoincidence<T>(T left, T right) => Error.Conflict(
        code: "General.NoCoincidence",
        description: $"The value '{left}' does not coincide with '{right}'.");

    /// <summary>
    /// Gets the creating or updating failed error.
    /// </summary>
    /// <value>
    /// The creating or updating failed error.
    /// </value>
    public static Error CreationOrUpdatingFailed => Error.Failure(
        code: "General.CreationOrUpdatingFailed",
        description: "Something was wrong. Try again later.");

    /// <summary>
    /// Gets the not null or empty property failed error.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <returns>Returns a not null or empty property error.</returns>
    public static Error NotNullOrEmptyProperty(string propertyName) => Error.Conflict(
        description: $"{propertyName} cannot be empty.");
}
