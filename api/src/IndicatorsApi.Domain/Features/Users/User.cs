﻿using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User model from the database table.
/// </summary>
public class User
    : Entity<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="password">User password.</param>
    public User(string id, string password)
        : base(id)
    {
        Password = password;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    public User()
        : base()
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s password.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s password.
    /// </value>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s salt.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s salt.
    /// </value>
#pragma warning disable CA1819 // Properties should not return arrays
    public byte[][]? Salt { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    /// <summary>
    /// Gets the <see cref="User"/>'s <see cref="ICollection{T}"/> roles.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s <see cref="ICollection{T}"/> roles.
    /// </value>
    public virtual ICollection<Role> Roles { get; } = new List<Role>();

    /// <summary>
    /// Add roles to an user.
    /// </summary>
    /// <param name="role"><see cref="Role"/> instance.</param>
    public void Add(Role role)
    {
        Roles.Add(role);
    }
}
