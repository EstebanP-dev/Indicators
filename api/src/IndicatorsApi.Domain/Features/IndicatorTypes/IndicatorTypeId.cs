using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.IndicatorTypes;

/// <summary>
/// IndicatorType id type.
/// </summary>
public sealed class IndicatorTypeId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorTypeId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private IndicatorTypeId(int value)
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
    /// Returns implicit an instance of <see cref="IndicatorTypeId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator IndicatorTypeId(int value) => ToIndicatorTypeId(value);

    /// <summary>
    /// Create a <see cref="IndicatorTypeId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="IndicatorTypeId"/>.</returns>
    public static IndicatorTypeId ToIndicatorTypeId(int value)
    {
        return new IndicatorTypeId(value);
    }
}
