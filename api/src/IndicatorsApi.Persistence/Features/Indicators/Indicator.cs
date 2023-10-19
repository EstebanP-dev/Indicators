using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Indicators;

/// <inheritdoc/>
internal sealed class IndicatorRepository
    : Repository<Indicator, int>, IIndicatorRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public IndicatorRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}

/// <inheritdoc/>
internal sealed class IndicatorConfiguration
    : IEntityTypeConfiguration<Indicator>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Indicator> builder)
    {
        builder
            .ToTable("indicador");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();
    }
}