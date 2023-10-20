using IndicatorsApi.Domain.Features.Variables;

namespace IndicatorsApi.Persistence.Features.Variables;

/// <inheritdoc/>
internal sealed class VariableConfiguration
    : IEntityTypeConfiguration<Variable>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Variable> builder)
    {
        builder.ToTable("variable");

        builder.HasKey(variable => variable.Id);

        builder.Property(variable => variable.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(variable => variable.Id)
            .IsUnique();

        builder.Property(variable => variable.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(variable => variable.CreationDate)
            .HasColumnName("fechacreacion")
            .IsRequired();

        builder.Property(variable => variable.CreatedBy)
            .HasColumnName("fkemailusuario")
            .IsRequired();
    }
}
