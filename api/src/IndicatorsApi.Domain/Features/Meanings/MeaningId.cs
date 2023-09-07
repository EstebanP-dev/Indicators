using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Meanings;

/// <summary>
/// Meaning id type.
/// </summary>
public sealed class MeaningId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeaningId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private MeaningId(int value)
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
    /// Returns implicit an instance of <see cref="MeaningId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator MeaningId(int value) => ToMeaningId(value);

    /// <summary>
    /// Create a <see cref="MeaningId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="MeaningId"/>.</returns>
    public static MeaningId ToMeaningId(int value)
    {
        return new MeaningId(value);
    }
}
