using IndicatorsApi.Domain.Features.Frequencies;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.SubSections;

namespace IndicatorsApi.Persistence.Features.Frequencies;

/// <inheritdoc/>
internal sealed class FrequencyConfiguration
    : IEntityTypeConfiguration<Frequency>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Frequency> builder)
    {
        builder
            .ToTable("frecuencia");

        builder
            .HasKey(articule => articule.Id);

        builder
            .Property(article => article.Id)
            .HasColumnName("id")
            .HasMaxLength(20)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(article => article.Description)
            .HasColumnName("descripcion")
            .HasMaxLength(200)
            .IsRequired();
    }
}