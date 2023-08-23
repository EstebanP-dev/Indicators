namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// Exception thrown when the user password salt is null.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class NullUserPasswordSaltException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullUserPasswordSaltException"/> class.
    /// </summary>
    /// <param name="email">User email.</param>
    public NullUserPasswordSaltException(string email)
        : base($"The user with email '{email}' have a salt column null.")
    {
    }
}
