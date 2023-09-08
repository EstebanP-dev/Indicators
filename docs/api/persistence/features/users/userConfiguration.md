# User Configuration

Esta clase que hereda de la interfaz [IEntityTypeConfiguration](https://learn.microsoft.com/en-us/ef/core/). Es encargada de hacer match con los modelos y la tabla en la base de datos. En esta clase, especificamos que columnas tiene la db y como se relacionan con los modelos.

**NOTA:**

- Al tener una relación de muchos a muchos, es necesario adaptar una configuración extra para obtener y guardar los roles asociados a la tabla usuarios, en el caso de este ejercicio.

```csharp
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
                value => UserId.ToUserId(value))
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
            .WithMany(role => role.Users)
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
                        .HasConversion(
                            userId => userId.Value,
                            value => UserId.ToUserId(value))
                        .HasColumnName("fkemail");

                    userRoleBuilder
                        .Property(userRole => userRole.RoleId)
                        .HasConversion(
                            roleId => roleId.Value,
                            value => RoleId.ToRoleId(value))
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
```
