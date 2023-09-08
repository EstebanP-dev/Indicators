# UserId

```csharp
/// <summary>
/// User id type.
/// </summary>
public sealed class UserId
    : ValueObject
{
    private UserId(string value)
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

    /// <summary>
    ///  Returns implicit an instance of <see cref="UserId"/>.
    /// </summary>
    /// <param name="value">Id value.</param>
    public static implicit operator UserId(string value) => ToUserId(value);

    /// <summary>
    /// Create an <see cref="UserId"/> instance.
    /// </summary>
    /// <param name="value">Id value.</param>
    /// <returns>Returns an <see cref="UserId"/> instance.</returns>
    public static UserId ToUserId(string value)
    {
        return new UserId(value);
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
```