using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Persistence.Features.Sources;

/// <inheritdoc/>
internal sealed class SourceConfiguration
    : IEntityTypeConfiguration<Source>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable("fuente");

        builder.HasKey(source => source.Id);

        builder
            .Property(source => source.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(source => source.Name)
            .HasColumnName("nombre")
            .HasMaxLength(2000)
            .IsRequired();
    }
}
