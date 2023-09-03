using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Persistence.Configurations;

/// <inheritdoc/>
internal sealed class UserRoleConfiguration
    : IEntityTypeConfiguration<UserRole>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("rol_usuario");

        builder.HasKey(user => user.UserId);
        builder.HasKey(user => user.RoleId);

        builder.Property(user => user.UserId)
            .HasColumnName("fkemail")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(user => user.UserId)
            .IsUnique();

        builder.Property(user => user.RoleId)
            .HasColumnName("fkidrol")
            .IsRequired();

        builder.HasIndex(user => user.RoleId);

        builder.HasOne(userRole => userRole.User)
            .WithMany()
            .HasForeignKey(userRole => userRole.UserId);

        builder.HasOne(userRole => userRole.Role)
            .WithMany()
            .HasForeignKey(userRole => userRole.RoleId);
    }
}
