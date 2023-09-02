namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Exception thrown when the role cannot be found by id.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class RoleOrRolesByUserIdCannotBeFoundException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleOrRolesByUserIdCannotBeFoundException"/> class.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="innerException">Inner Exception.</param>
    public RoleOrRolesByUserIdCannotBeFoundException(string userId, Exception? innerException = null)
        : base($"The roles cannot be found by user id '{userId}'.", innerException)
    {
    }
}