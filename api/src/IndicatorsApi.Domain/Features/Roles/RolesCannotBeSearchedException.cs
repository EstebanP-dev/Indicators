namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Exception thrown when the roles cannot be searched.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public sealed class RolesCannotBeSearchedException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RolesCannotBeSearchedException"/> class.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public RolesCannotBeSearchedException(Exception? innerException = null)
        : base(message: "The roles cannot be searched.", innerException: innerException)
    {
    }
}
