using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Indicators;

/// <inheritdoc/>
internal sealed class IndicatorTypeRepository
    : Repository<IndicatorType, int>, IIndicatorTypeRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorTypeRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public IndicatorTypeRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}

/// <inheritdoc/>
internal sealed class IndicatorTypeConfiguration
    : IEntityTypeConfiguration<IndicatorType>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<IndicatorType> builder)
    {
        builder.ToTable("tipoindicador");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
