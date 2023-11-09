using IndicatorsApi.Domain.Features.MeasurementUnits;

namespace IndicatorsApi.Persistence.Features.MeasurementUnits;

/// <inheritdoc/>
internal sealed class MeasurementUnitConfiguration
    : IEntityTypeConfiguration<MeasurementUnit>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.ToTable("unidadmedicion");

        builder.HasKey(section => section.Id);

        builder.Property(section => section.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(section => section.Id)
            .IsUnique();

        builder.Property(section => section.Description)
            .HasColumnName("descripcion")
            .HasMaxLength(200)
            .IsRequired();
    }
}
