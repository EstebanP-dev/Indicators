namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User model from the database table.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the <see cref="User"/>'s id/email.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s id/email.
    /// </value>
    required public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s password.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s password.
    /// </value>
    required public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the <see cref="User"/>'s salt.
    /// </summary>
    /// <value>
    /// The <see cref="User"/>'s salt.
    /// </value>
#pragma warning disable CA1819 // Properties should not return arrays
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
    public byte[][]? Salt { get; set; }
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
#pragma warning restore CA1819 // Properties should not return arrays
}
