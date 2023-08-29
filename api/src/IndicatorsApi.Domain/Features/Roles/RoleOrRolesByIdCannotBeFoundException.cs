namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Exception thrown when the role cannot be found by id.
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2237 // Mark ISerializable types with serializable
public class RoleOrRolesByIdCannotBeFoundException
#pragma warning restore CA2237 // Mark ISerializable types with serializable
#pragma warning restore CA1032 // Implement standard exception constructors
    : BaseException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleOrRolesByIdCannotBeFoundException"/> class.
    /// </summary>
    /// <param name="ids">Role ids.</param>
    /// <param name="innerException">Inner Exception.</param>
    public RoleOrRolesByIdCannotBeFoundException(int[] ids, Exception? innerException = null)
        : base($"The roles with the ids '{string.Join(",", ids)}' cannot be found.", innerException)
    {
    }
}