using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Persistence.Features.Sections;

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
            .HasColumnName("id")
            .HasMaxLength(2)
            .IsRequired();

        builder.HasIndex(section => section.Id)
            .IsUnique();

        builder.Property(section => section.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
