namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// Exception thrown when the user cannot be found by email.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class UserByEmailCannotBeFoundException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserByEmailCannotBeFoundException"/> class.
    /// </summary>
    /// <param name="email">User email.</param>
    public UserByEmailCannotBeFoundException(string email)
        : base($"The user with the email '{email}' cannot be found.")
    {
    }
}
