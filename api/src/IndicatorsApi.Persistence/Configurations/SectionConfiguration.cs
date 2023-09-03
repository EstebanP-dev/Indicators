using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Persistence.Configurations;

/// <inheritdoc/>
internal sealed class SectionConfiguration
    : IEntityTypeConfiguration<Section>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("seccion");

        builder.HasKey(section => section.Id);

        builder.Property(section => section.Id)
            .HasConversion(
                sectionId => sectionId.Value,
                value => new SectionId(value))
            .HasColumnName("id")
            .IsRequired();

        builder.HasIndex(section => section.Id)
            .IsUnique();

        builder.Property(section => section.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
