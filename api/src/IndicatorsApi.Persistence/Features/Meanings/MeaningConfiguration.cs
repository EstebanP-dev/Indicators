using IndicatorsApi.Domain.Features.Meanings;

namespace IndicatorsApi.Persistence.Features.Meanings;

/// <inheritdoc/>
internal sealed class MeaningConfiguration
    : IEntityTypeConfiguration<Meaning>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Meaning> builder)
    {
        builder.ToTable("sentido");

        builder.HasKey(section => section.Id);

        builder.Property(section => section.Id)
            .HasConversion(
                sectionId => sectionId.Value,
                value => MeaningId.ToMeaningId(value))
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(section => section.Id)
            .IsUnique();

        builder.Property(section => section.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
