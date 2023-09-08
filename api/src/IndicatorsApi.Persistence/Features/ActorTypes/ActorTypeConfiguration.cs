using IndicatorsApi.Domain.Features.ActorTypes;

namespace IndicatorsApi.Persistence.Features.ActorTypes;

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
            .HasConversion(
                actorTypeId => actorTypeId.Value,
                value => ActorTypeId.ToActorTypeId(value))
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
