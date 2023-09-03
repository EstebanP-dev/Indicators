using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User role model from the database table.
/// </summary>
public class UserRole
{
    /// <summary>
    /// Gets the <see cref="User"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s id.
    /// </value>
    required public UserId UserId { get; init; }

    /// <summary>
    /// Gets the <see cref="Role"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s id.
    /// </value>
    required public RoleId RoleId { get; init; }

    /// <summary>
    /// Gets user instance.
    /// </summary>
    /// <value>
    /// User instance.
    /// </value>
    required public User User { get; init; }

    /// <summary>
    /// Gets role instance.
    /// </summary>
    /// <value>
    /// Role instance.
    /// </value>
    required public Role Role { get; init; }
}
