using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User role model from the database table.
/// </summary>
public class UserRole
{
    /// <summary>
    /// Gets or sets the <see cref="User"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s id.
    /// </value>
    required public string FkEmail { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Role"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s id.
    /// </value>
    required public int FkRol { get; set; }
}
