namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Exception thrown when the sections cannot be searched.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public sealed class SectionsCannotBeSearchedException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SectionsCannotBeSearchedException"/> class.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public SectionsCannotBeSearchedException(Exception? innerException = null)
        : base(message: "The sections cannot be searched.", innerException: innerException)
    {
    }
}
