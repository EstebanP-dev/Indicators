namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// Exception thrown when the users cannot be searched.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public sealed class UsersCannotBeSearchedException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UsersCannotBeSearchedException"/> class.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public UsersCannotBeSearchedException(Exception? innerException = null)
        : base("The users cannot be searched.", innerException: innerException)
    {
    }
}
