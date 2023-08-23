using IndicatorsApi.Domain.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndicatorsApi.Persistence.Configurations;

/// <inheritdoc/>
internal sealed class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(user => user.Email);

        builder.Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.Property(user => user.Password)
            .HasColumnName("contrasena")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(user => user.Salt)
            .HasColumnName("salt")
            .HasColumnType("bytea[]");
    }
}

/// <inheritdoc/>
internal sealed class UserRoleConfiguration
    : IEntityTypeConfiguration<UserRole>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("rol_usuario");

        builder.HasKey(user => user.FkEmail);
        builder.HasKey(user => user.FkRol);

        builder.Property(user => user.FkEmail)
            .HasColumnName("fkemail")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(user => user.FkEmail)
            .IsUnique();

        builder.Property(user => user.FkRol)
            .HasColumnName("fkidrol")
            .IsRequired();

        builder.HasIndex(user => user.FkRol);
    }
}
