using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Persistence.Configurations;

/// <inheritdoc/>
internal sealed class SourceConfiguration
    : IEntityTypeConfiguration<Source>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable("fuente");

        builder.HasKey(source => source.Id);

        builder.Property(source => source.Id)
            .HasConversion(
                sourceId => sourceId.Value,
                value => SourceId.ToSourceId(value))
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(source => source.Id)
            .IsUnique();

        builder.Property(source => source.Name)
            .HasColumnName("nombre")
            .HasMaxLength(2000)
            .IsRequired();
    }
}
