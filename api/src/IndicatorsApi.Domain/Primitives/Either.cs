namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// Represent an instance either of <typeparamref name="TLeft"/> or <typeparamref name="TRight"/>.
/// </summary>
/// <typeparam name="TLeft">Left return value.</typeparam>
/// <typeparam name="TRight">Right return value.</typeparam>
public class Either<TLeft, TRight>
{
    private readonly TLeft? _left;
    private readonly TRight? _right;
    private readonly bool _isLeft;

    /// <summary>
    /// Initializes a new instance of the <see cref="Either{TLeft, TRight}"/> class.
    /// </summary>
    /// <param name="left">Left value.</param>
    public Either(TLeft left)
    {
        _left = left;
        _isLeft = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Either{TLeft, TRight}"/> class.
    /// </summary>
    /// <param name="right">Right value.</param>
    public Either(TRight right)
    {
        _right = right;
        _isLeft = false;
    }

    /// <summary>
    /// Returns a instance of a new value base on the result of the parameter right or left functions.
    /// </summary>
    /// <typeparam name="T">New return type.</typeparam>
    /// <param name="left">Function base on left value.</param>
    /// <param name="right">Function base on right value.</param>
    /// <returns>New return value.</returns>
    public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right)
#pragma warning disable CA1062 // Validate arguments of public methods
        => _isLeft
        ? left(_left!)
        : right(_right!);
#pragma warning restore CA1062 // Validate arguments of public methods
}
