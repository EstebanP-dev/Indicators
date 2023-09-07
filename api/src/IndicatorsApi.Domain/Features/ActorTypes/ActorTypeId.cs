using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.ActorTypes;

/// <summary>
/// ActorType id type.
/// </summary>
public sealed class ActorTypeId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActorTypeId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private ActorTypeId(int value)
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
    /// Returns implicit an instance of <see cref="ActorTypeId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator ActorTypeId(int value) => ToActorTypeId(value);

    /// <summary>
    /// Create a <see cref="ActorTypeId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="ActorTypeId"/>.</returns>
    public static ActorTypeId ToActorTypeId(int value)
    {
        return new ActorTypeId(value);
    }
}
