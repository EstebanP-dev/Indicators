namespace IndicatorsApi.Domain.Features.Users;

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
