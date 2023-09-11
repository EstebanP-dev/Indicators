using IndicatorsApi.Domain.Features.SubSections;

namespace IndicatorsApi.Persistence.Features.SubSections;

/// <inheritdoc/>
internal sealed class SubSectionConfiguration
    : IEntityTypeConfiguration<SubSection>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<SubSection> builder)
    {
        builder.ToTable("subseccion");

        builder.HasKey(subsection => subsection.Id);

        builder.Property(subsection => subsection.Id)
            .HasConversion(
                subsectionId => subsectionId.Value,
                value => SubSectionId.ToSubSectionId(value))
            .HasColumnName("id")
            .IsRequired();

        builder.HasIndex(subsection => subsection.Id)
            .IsUnique();

        builder.Property(subsection => subsection.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
