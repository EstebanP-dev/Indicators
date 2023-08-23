namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// Exception thrown when the user cannot be found by email.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class InvalidUserCredentialsException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUserCredentialsException"/> class.
    /// </summary>
    public InvalidUserCredentialsException()
        : base($"The credentials are not valid.")
    {
    }
}
