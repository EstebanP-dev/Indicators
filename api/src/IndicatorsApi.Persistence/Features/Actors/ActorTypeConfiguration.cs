using IndicatorsApi.Domain.Features.Actors;

namespace IndicatorsApi.Persistence.Features.Actors;

/// <inheritdoc/>
internal sealed class ActorTypeConfiguration
    : IEntityTypeConfiguration<ActorType>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ActorType> builder)
    {
        builder.ToTable("tipoactor");

        builder.HasKey(actorType => actorType.Id);

        builder.Property(actorType => actorType.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(actorType => actorType.Id)
            .IsUnique();

        builder.Property(actorType => actorType.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}
