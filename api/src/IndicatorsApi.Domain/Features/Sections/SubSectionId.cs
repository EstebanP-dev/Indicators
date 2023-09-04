using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// SubSection id type.
/// </summary>
public sealed class SubSectionId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubSectionId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private SubSectionId(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public string Value { get; private set; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>
    /// Returns implicit an instance of <see cref="SubSectionId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator SubSectionId(string value) => ToSubSectionId(value);

    /// <summary>
    /// Create a <see cref="SubSectionId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="SubSectionId"/>.</returns>
    public static SubSectionId ToSubSectionId(string value)
    {
        return new SubSectionId(value);
    }
}