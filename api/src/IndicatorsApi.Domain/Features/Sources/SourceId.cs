using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sources;

/// <summary>
/// Source id type.
/// </summary>
public sealed class SourceId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SourceId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private SourceId(int value)
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
    /// Returns implicit an instance of <see cref="SourceId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator SourceId(int value) => ToSourceId(value);

    /// <summary>
    /// Create a <see cref="SourceId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="SourceId"/>.</returns>
    public static SourceId ToSourceId(int value)
    {
        return new SourceId(value);
    }
}
