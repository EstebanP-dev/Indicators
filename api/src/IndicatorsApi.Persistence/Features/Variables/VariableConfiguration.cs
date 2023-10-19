using IndicatorsApi.Domain.Features.Variables;

namespace IndicatorsApi.Persistence.Features.Variables;

/// <inheritdoc/>
internal sealed class VariableConfiguration
    : IEntityTypeConfiguration<Variable>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Variable> builder)
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

        builder.Property(actorType => actorType.CreationDate)
            .HasColumnName("fechacreacion")
            .IsRequired();

        builder.Property(actorType => actorType.CreatedBy)
            .HasColumnName("fkemailusuario")
            .IsRequired();
    }
}
