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

        builder.HasKey(meaning => meaning.Id);

        builder.Property(meaning => meaning.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(meaning => meaning.Id)
            .IsUnique();

        builder.Property(meaning => meaning.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
