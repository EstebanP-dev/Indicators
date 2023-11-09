# ValueObject

Esta clase al igual que [Entity{T}](entityT.md), hereda de la interfaz [IEquatable](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=net-7.0); se diferencian en que [Entity{T}](entityT.md) hace las comparaciones con una subclase que equivale al id de la entidad.

Para este [ValueObject](#valueobject), creamos una interfaz en la misma carpeta [Primitives](primitives.md).

```csharp
/// <summary>
/// <see cref="IEquatable{T}"/> abtract methods.
/// </summary>
/// <typeparam name="TClass"><see cref="IValueObject{T}"/> implement class.</typeparam>
public interface IValueObject<TClass> : IEquatable<TClass>
    where TClass : IValueObject<TClass>
{
}
```

```csharp
/// <summary>
/// <see cref="IValueObject{T}"/> implementation.
/// </summary>
public abstract class ValueObject : IValueObject<ValueObject>
{
    /// <summary>
    /// Equals algorithm comparative.
    /// </summary>
    /// <param name="left">Left value.</param>
    /// <param name="right">Right value.</param>
    /// <returns>Returns if the values are the same.</returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    /// <summary>
    /// Not equals algorithm comparative.
    /// </summary>
    /// <param name="left">Left value.</param>
    /// <param name="right">Right value.</param>
    /// <returns>Returns if the values are the diferents.</returns>
    public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

    /// <inheritdoc/>
    public bool Equals(ValueObject? other) => other is not null && GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        if (obj is not ValueObject valueObject)
        {
            return false;
        }

        return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (object obj in GetAtomicValues())
        {
            hashCode.Add(obj);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Gets the atomic values.
    /// </summary>
    /// <returns>Return an <see cref="IEnumerable{T}"/> of values.</returns>
    protected abstract IEnumerable<object> GetAtomicValues();
}
```

