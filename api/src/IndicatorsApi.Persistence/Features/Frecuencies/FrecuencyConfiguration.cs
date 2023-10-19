using IndicatorsApi.Domain.Features.Frecuencies;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.SubSections;

namespace IndicatorsApi.Persistence.Features.Frecuencies;

/// <inheritdoc/>
internal sealed class FrecuencyConfiguration
    : IEntityTypeConfiguration<Frecuency>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Frecuency> builder)
    {
        builder.ToTable("frecuencia");

        builder.HasKey(articule => articule.Id);

        builder.Property(article => article.Id)
            .HasColumnName("id")
            .HasMaxLength(20)
            .ValueGeneratedOnAdd()
            .IsRequired();
    }
}