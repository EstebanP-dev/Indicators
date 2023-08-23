using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain.Features.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.Persistence;

/// <summary>
/// Application database context.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">Instance of <see cref="DbContextOptions{ApplicationDbContext}"/>.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<User> Users { get; set; }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        modelBuilder.ApplyConfigurationsFromAssembly(PersistenceAssembly.Assembly);
#pragma warning restore CA1062 // Validate arguments of public methods
    }
}
