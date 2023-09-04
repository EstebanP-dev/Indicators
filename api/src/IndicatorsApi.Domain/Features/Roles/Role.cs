using IndicatorsApi.Domain.Features.Users;
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
    /// <param name="name">Role name.</param>
    public Role(RoleId id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the <see cref="Role"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets a collection of users.
    /// </summary>
    /// <value>
    /// A collection of users.
    /// </value>
    public ICollection<User> Users { get; } = new List<User>();
}
