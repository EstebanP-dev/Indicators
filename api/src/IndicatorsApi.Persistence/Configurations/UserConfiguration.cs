using IndicatorsApi.Domain.Features.Roles;
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

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value))
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

        builder.HasMany(user => user.Roles)
            .WithMany()
            .UsingEntity<UserRole>();
    }
}