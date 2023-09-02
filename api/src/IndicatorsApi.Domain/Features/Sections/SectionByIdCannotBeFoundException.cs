namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Exception thrown when the seccion cannor be found by id.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public sealed class SectionByIdCannotBeFoundException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SectionByIdCannotBeFoundException"/> class.
    /// </summary>
    /// <param name="id">Section id.</param>
    /// <param name="innerException">Inner exception.</param>
    public SectionByIdCannotBeFoundException(int id, Exception? innerException = null)
        : base($"The section with id '{id}' cannot be found.", innerException)
    {
    }
}
