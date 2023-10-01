using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Articles;

/// <summary>
/// Article id type.
/// </summary>
public sealed class ArticleId
    : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleId"/> class.
    /// </summary>
    /// <param name="value">Value instance.</param>
    private ArticleId(string value)
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
    /// Returns implicit an instance of <see cref="ArticleId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator ArticleId(string value) => ToArticleId(value);

    /// <summary>
    /// Create a <see cref="ArticleId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an instance of <see cref="ArticleId"/>.</returns>
    public static ArticleId ToArticleId(string value)
    {
        return new ArticleId(value);
    }
}