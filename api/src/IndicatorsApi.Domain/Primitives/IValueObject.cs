namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// <see cref="IEquatable{T}"/> abtract methods.
/// </summary>
/// <typeparam name="TClass"><see cref="IValueObject{T}"/> implement class.</typeparam>
public interface IValueObject<TClass> : IEquatable<TClass>
    where TClass : IValueObject<TClass>
{
}