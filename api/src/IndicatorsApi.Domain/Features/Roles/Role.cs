namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Role model from the database table.
/// </summary>
public class Role
{
    /// <summary>
    /// Gets or sets the <see cref="Role"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s id.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Role"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s name.
    /// </value>
    required public string Name { get; set; }
}
