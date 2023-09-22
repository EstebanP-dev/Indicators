using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.MeasurementUnits;

/// <summary>
/// MeasurementUnit id type.
/// </summary>
public sealed class MeasurementUnitId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeasurementUnitId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private MeasurementUnitId(int value)
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
    /// Returns implicit an instance of <see cref="MeasurementUnitId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator MeasurementUnitId(int value) => ToMeasurementUnitId(value);

    /// <summary>
    /// Create a <see cref="MeasurementUnitId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="MeasurementUnitId"/>.</returns>
    public static MeasurementUnitId ToMeasurementUnitId(int value)
    {
        return new MeasurementUnitId(value);
    }
}
