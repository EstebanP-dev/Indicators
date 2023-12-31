﻿using IndicatorsApi.Domain.Features.Roles;

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
    required public string UserId { get; init; }

    /// <summary>
    /// Gets the <see cref="Role"/>'s id.
    /// </summary>
    /// <value>
    /// The <see cref="Role"/>'s id.
    /// </value>
    required public int RoleId { get; init; }
}
