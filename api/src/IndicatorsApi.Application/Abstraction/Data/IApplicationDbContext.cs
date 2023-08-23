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
}
