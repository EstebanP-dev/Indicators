using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Role model from the database table.
/// </summary>
public class Role
    : Entity<RoleId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class.
    /// </summary>
    /// <param name="id">Role id.</param>
    public Role(RoleId id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Role"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s name.
    /// </value>
    required public string Name { get; set; }
}
