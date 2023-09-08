using IndicatorsApi.Domain.Features.IndicatorTypes;

namespace IndicatorsApi.Persistence.Features.IndicatorTypes;

/// <inheritdoc/>
internal sealed class IndicatorTypeConfiguration
    : IEntityTypeConfiguration<IndicatorType>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<IndicatorType> builder)
    {
        builder.ToTable("tipoindicador");

        builder.HasKey(indicatorType => indicatorType.Id);

        builder.Property(indicatorType => indicatorType.Id)
            .HasConversion(
                indicatorTypeId => indicatorTypeId.Value,
                value => IndicatorTypeId.ToIndicatorTypeId(value))
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(indicatorType => indicatorType.Id)
            .IsUnique();

        builder.Property(indicatorType => indicatorType.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
