namespace IndicatorsApi.Domain.Errors;

/// <summary>
/// Section domain errors.
/// </summary>
public static class SectionErrors
{
    /// <summary>
    /// Gets the creation or updation failed error.
    /// </summary>
    /// <value>
    /// The creation or updation failed error.
    /// </value>
    public static Error CreationOrUpdatingFailed => Error.Failure(
        code: "Section.CreationOrUpdatingFailed",
        description: "Something was wrong. Try again later.");
}
