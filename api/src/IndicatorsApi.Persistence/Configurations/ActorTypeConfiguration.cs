using IndicatorsApi.Domain.Features.ActorTypes;

namespace IndicatorsApi.Persistence.Configurations;

/// <inheritdoc/>
internal sealed class ActorTypeConfiguration
    : IEntityTypeConfiguration<ActorType>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ActorType> builder)
    {
        builder.ToTable("tipoactor");

        builder.HasKey(section => section.Id);

        builder.Property(section => section.Id)
            .HasConversion(
                sectionId => sectionId.Value,
                value => ActorTypeId.ToActorTypeId(value))
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
