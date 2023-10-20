using IndicatorsApi.Domain.Features.Indicators;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Indicators;

/// <inheritdoc/>
internal sealed class IndicatorResultRepository
    : Repository<IndicatorResult, int>, IIndicatorResultRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorResultRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public IndicatorResultRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}

/// <inheritdoc/>
internal sealed class IndicatorResultConfiguration
    : IEntityTypeConfiguration<IndicatorResult>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<IndicatorResult> builder)
    {
        builder.ToTable("resultadoindicador");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Result)
            .HasColumnName("resultado")
            .IsRequired();

        builder.Property(x => x.CalculusDate)
            .HasColumnName("fechacalculo")
            .IsRequired();

        builder.Property(x => x.IndicatorId)
            .HasColumnName("fkidindicador")
            .IsRequired();
    }
}
