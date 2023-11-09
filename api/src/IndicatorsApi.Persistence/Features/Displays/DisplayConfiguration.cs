using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Persistence.Features.Displays;

/// <inheritdoc/>
internal sealed class DisplayConfiguration
    : IEntityTypeConfiguration<Display>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Display> builder)
    {
        builder.ToTable("represenvisual");

        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
