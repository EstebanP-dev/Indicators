using IndicatorsApi.Application.Abstraction.Data;
using IndicatorsApi.Domain.Features.Articles;
using IndicatorsApi.Domain.Features.Frequencies;
using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Persistence;

/// <summary>
/// Application database context.
/// </summary>
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
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
    public DbSet<Role> Roles { get; set; }

    /// <inheritdoc/>
    public DbSet<Section> Sections { get; set; }

    /// <inheritdoc/>
    public DbSet<SubSection> SubSections { get; set; }

    /// <inheritdoc/>
    public DbSet<Source> Sources { get; set; }

    /// <inheritdoc/>
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    /// <inheritdoc/>
    public DbSet<Article> Articles { get; set; }

    /// <inheritdoc/>
    public DbSet<Frequency> Frequencies { get; set; }

    /// <inheritdoc/>
    public DbSet<Indicator> Indicators { get; set; }

    /// <inheritdoc/>
    public DbSet<IndicatorResult> IndicatorResults { get; set; }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
#pragma warning restore CA1062 // Validate arguments of public methods
    }
}
