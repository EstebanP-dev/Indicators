namespace IndicatorsApi.Domain.Errors;

/// <summary>
/// User domain errors.
/// </summary>
public static class UserErrors
{
    /// <summary>
    /// Gets the not found error.
    /// </summary>
    /// <value>
    /// Not found error.
    /// </value>
    public static Error NotFound => Error.NotFound(
        code: "User.NotFound",
        description: "The user was not found.");

    /// <summary>
    /// Gets the bulk not found error.
    /// </summary>
    /// <value>
    /// The bulk not found error.
    /// </value>
    public static Error BulkNotFound => Error.Unexpected(
        code: "User.BulkNotFound",
        description: "Something was wrong. Try again later.");

    /// <summary>
    /// Gets the duplicate user email error.
    /// </summary>
    /// <returns>Result error.</returns>
    /// <value>
    /// The duplicate user email error.
    /// </value>
    public static Error DuplicateEmail => Error.Conflict(
        code: "User.DuplicateEmail",
        description: "The email already exists.");
}
