using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Displays;

/// <summary>
/// Display id type.
/// </summary>
public sealed class DisplayId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private DisplayId(int value)
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
    /// Returns implicit an instance of <see cref="DisplayId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator DisplayId(int value) => ToDisplayId(value);

    /// <summary>
    /// Create a <see cref="DisplayId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="DisplayId"/>.</returns>
    public static DisplayId ToDisplayId(int value)
    {
        return new DisplayId(value);
    }
}
