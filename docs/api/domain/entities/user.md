# User


```csharp
/// <summary>
/// User model from the database table.
/// </summary>
public class User
    : Entity<UserId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="password">User password.</param>
    public User(UserId id, string password)
        : base(id)
    {
        Password = password;
    }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s password.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s password.
    /// </value>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s salt.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s salt.
    /// </value>

    public byte[][]? Salt { get; set; }

    /// <summary>
    /// Gets the <see cref="User"/>'s <see cref="IReadOnlyList{T}"/> roles.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s <see cref="IReadOnlyList{T}"/> roles.
    /// </value>
    public virtual ICollection<Role> Roles { get; } = new List<Role>();

    /// <summary>
    /// Add roles to an user.
    /// </summary>
    /// <param name="role"><see cref="Role"/> instance.</param>
    public void Add(Role role)
    {
        Roles.Add(role);
    }
}
```

**NOTA:**

- Se agregó un nuevo campo a la base de datos de tipo *bytea[]* (En la db). Este campo es usado para el método de encriptación [Salting Hash](https://code-maze.com/csharp-hashing-salting-passwords-best-practices/).
