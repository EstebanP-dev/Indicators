using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Errors;

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
#pragma warning disable CA1308 // Normalize strings to uppercase
        => Error.NotFound(
        description: $"The {typeof(T).Name.ToLowerInvariant()} was not found.");
#pragma warning restore CA1308 // Normalize strings to uppercase

    /// <summary>
    /// Gets the bulk not found error.
    /// </summary>
    /// <value>
    /// The bulk not found error.
    /// </value>
    public static Error BulkNotFound => Error.Unexpected(
        description: "Something was wrong. Try again later.");
}
