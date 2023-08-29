using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Application.Abstraction.Data;

/// <summary>
/// Application database context interface.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets or sets the user table.
    /// </summary>
    /// <value>
    /// User table.
    /// </value>
    DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the role table.
    /// </summary>
    /// <value>
    /// The role table.
    /// </value>
    DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Gets or sets the user role table.
    /// </summary>
    /// <value>
    /// The user role table.
    /// </value>
    DbSet<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Gets or sets the section table.
    /// </summary>
    /// <value>
    /// The section table.
    /// </value>
    DbSet<Section> Sections { get; set; }

    /// <summary>
    /// Gets or sets the sub section table.
    /// </summary>
    /// <value>
    /// The sub section table.
    /// </value>
    DbSet<SubSection> SubSections { get; set; }
}
