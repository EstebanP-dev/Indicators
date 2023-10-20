using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Persistence.Features.Users;

/// <inheritdoc/>
internal sealed class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasColumnName("email")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(user => user.Id)
            .IsUnique();

        builder.Property(user => user.Password)
            .HasColumnName("contrasena")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(user => user.Salt)
            .HasColumnName("salt")
            .HasColumnType("bytea[]");

        builder
            .HasMany(user => user.Roles)
            .WithMany()
            .UsingEntity<UserRole>(
                "rol_usuario",
                roleBuilder => roleBuilder
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey(userRole => userRole.RoleId)
                    .HasConstraintName("rol_usuario_ibfk_2"),
                userBuilder => userBuilder
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey(userRole => userRole.UserId)
                    .HasConstraintName("rol_usuario_ibfk_1"),
                userRoleBuilder =>
                {
                    userRoleBuilder
                        .Property(userRole => userRole.UserId)
                        .HasColumnName("fkemail");

                    userRoleBuilder
                        .Property(userRole => userRole.RoleId)
                        .HasColumnName("fkidrol");

                    userRoleBuilder
                        .HasKey(userRole => userRole.UserId)
                        .HasName("rol_usuario_pkey");
                    userRoleBuilder
                        .HasKey(userRole => userRole.RoleId)
                        .HasName("rol_usuario_pkey");

                    userRoleBuilder.ToTable("rol_usuario");
                });
    }
}