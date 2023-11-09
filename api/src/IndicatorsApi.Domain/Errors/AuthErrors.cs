namespace IndicatorsApi.Domain.Errors;

/// <summary>
/// Auth domain errors.
/// </summary>
public static class AuthErrors
{
    /// <summary>
    /// Gets the invalid credentials error.
    /// </summary>
    /// <value>
    /// The invalid credentials error.
    /// </value>
    public static Error InvalidCredentials => Error.Conflict(
        code: "Auth.InvalidCredentials",
        description: "The username or password are not valid. Try again later.");

    /// <summary>
    /// Gets the restore password error.
    /// </summary>
    /// <value>
    /// The restore password error.
    /// </value>
    public static Error RestorePassword => Error.Conflict(
        code: "Auth.RestorePassword",
        description: "It's necessary to restore the password.");
}