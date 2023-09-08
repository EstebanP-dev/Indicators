using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Persistence.Features.Roles;

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
            .HasConversion(
                roleId => roleId.Value,
                value => RoleId.ToRoleId(value))
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(role => role.Id)
            .IsUnique();

        builder.Property(role => role.Name)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();
    }
}
