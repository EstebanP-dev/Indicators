using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User model from the database table.
/// </summary>
public sealed class User : Entity<UserId>
{
    private readonly HashSet<Role> _roles = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">User id.</param>
    public User(UserId id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s password.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s password.
    /// </value>
    required public string Password { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s salt.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s salt.
    /// </value>
#pragma warning disable CA1819 // Properties should not return arrays
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
    public byte[][]? Salt { get; set; }
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
#pragma warning restore CA1819 // Properties should not return arrays

    /// <summary>
    /// Gets the <see cref="User"/>'s <see cref="IReadOnlyList{T}"/> roles.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s <see cref="IReadOnlyList{T}"/> roles.
    /// </value>
#pragma warning disable S2365 // Properties should not make collection or array copies
    public IReadOnlyList<Role> Roles => _roles.ToList();
#pragma warning restore S2365 // Properties should not make collection or array copies

    /// <summary>
    /// Add roles to an user.
    /// </summary>
    /// <param name="role"><see cref="Role"/> instance.</param>
    public void Add(Role role)
    {
        _roles.Add(role);
    }
}
