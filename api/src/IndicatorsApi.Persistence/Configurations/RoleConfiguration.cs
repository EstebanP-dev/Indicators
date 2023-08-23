using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.Persistence.Configurations;

/// <inheritdoc/>
internal sealed class RoleConfiguration
    : IEntityTypeConfiguration<Role>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("rol");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.HasIndex(role => role.Id)
            .IsUnique();

        builder.Property(role => role.Name)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();
    }
}
