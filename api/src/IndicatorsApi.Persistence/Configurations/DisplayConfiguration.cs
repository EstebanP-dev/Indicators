using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Persistence.Configurations;

/// <inheritdoc/>
internal sealed class DisplayConfiguration
    : IEntityTypeConfiguration<Display>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Display> builder)
    {
        builder.ToTable("represenvisual");

        builder.HasKey(section => section.Id);

        builder.Property(section => section.Id)
            .HasConversion(
                sectionId => sectionId.Value,
                value => DisplayId.ToDisplayId(value))
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
