using IndicatorsApi.Domain.Features.Actors;

namespace IndicatorsApi.Persistence.Features.Actors;

/// <inheritdoc/>
internal sealed class ActorConfiguration
    : IEntityTypeConfiguration<Actor>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.ToTable("actor");

        builder.HasKey(actor => actor.Id);

        builder.Property(actor => actor.Id)
            .HasColumnName("id")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(actor => actor.Id)
            .IsUnique();

        builder.Property(actor => actor.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(actor => actor.ActorTypeId)
            .HasColumnName("fkidtipoactor")
            .IsRequired();

        builder.HasOne(actor => actor.ActorType)
            .WithMany()
            .HasForeignKey(actor => actor.ActorTypeId)
            .HasConstraintName("actor_ibfk_1");
    }
}
