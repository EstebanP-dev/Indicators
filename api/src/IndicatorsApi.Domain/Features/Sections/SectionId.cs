﻿using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Section id type.
/// </summary>
public sealed class SectionId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SectionId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private SectionId(string value)
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
    /// Returns implicit an instance of <see cref="SectionId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator SectionId(string value) => ToSectionId(value);

    /// <summary>
    /// Create a <see cref="SectionId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="SectionId"/>.</returns>
    public static SectionId ToSectionId(string value)
    {
        return new SectionId(value);
    }
}