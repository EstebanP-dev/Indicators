using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Role id type.
/// </summary>
public sealed class RoleId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private RoleId(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public int Value { get; private set; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>
    /// Returns implicit an instance of <see cref="RoleId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator RoleId(int value) => ToRoleId(value);

    /// <summary>
    /// Create a <see cref="RoleId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="RoleId"/>.</returns>
    public static RoleId ToRoleId(int value)
    {
        return new RoleId(value);
    }
}