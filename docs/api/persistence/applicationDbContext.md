# ApplicationDbContext

Es la implementación tanto de la la interfaz [IApplicationDbContext](./../application/abstractions/data/IapplicationDbContext.md), como de la clase **DbContext de [EntityFramework](https://learn.microsoft.com/en-us/ef/core/).

```csharp
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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(PersistenceAssembly.Assembly);
    }
}
```
