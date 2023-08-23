namespace IndicatorsApi.Domain.Primitives;

/// <summary>
/// Mapping of process results.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>
#pragma warning disable CA1819 // Properties should not return arrays
    public Error[] Errors { get; } = Array.Empty<Error>();
#pragma warning restore CA1819 // Properties should not return arrays

    /// <summary>
    /// Gets a value indicating whether the result is success.
    /// </summary>
    /// <value>
    /// A value indicating whether the result is success.
    /// </value>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the result is failure.
    /// </summary>
    /// <value>
    /// A value indicating whether the result is failure.
    /// </value>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">Result success value.</param>
    /// <param name="error">Result error.</param>
    protected internal Result(bool isSuccess, Error? error = null)
    {
        if ((isSuccess && error is not null) || (!isSuccess && error is null))
        {
            throw new InvalidErrorValueObjectHandlerException();
        }

        if (error is not null)
        {
            Errors = new[] { error };
        }

        IsSuccess = isSuccess;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">Result success value.</param>
    /// <param name="errors">Result errors.</param>
    protected internal Result(bool isSuccess, Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    /// <summary>
    /// Gets a success result.
    /// </summary>
    /// <returns>Returns a success result.</returns>
    public static Result Success() => new(true);

    /// <summary>
    /// Gets a success result with a return value.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="value">Result value.</param>
    /// <returns>Returns a success result.</returns>
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true);

    /// <summary>
    /// Gets a failure value with an error.
    /// </summary>
    /// <param name="error">Result error.</param>
    /// <returns>Returns a failure result.</returns>
    public static Result Failure(Error? error) => new(false, error);

    /// <summary>
    /// Gets a failure result with errors.
    /// </summary>
    /// <param name="errors">Result errors.</param>
    /// <returns>Returns a failure result.</returns>
    public static Result Failure(Error[] errors) => new(false, errors);

    /// <summary>
    /// Gets a failure result with an error.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="error">Result error.</param>
    /// <returns>Returns a failure result.</returns>
    public static Result<TValue> Failure<TValue>(Error? error) => new(default!, false, error);

    /// <summary>
    /// Gets a failure result with errors.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="errors">Result errors.</param>
    /// <returns>Returns a failure result.</returns>
    public static Result<TValue> Failure<TValue>(Error[] errors) => new(default!, false, errors);

    /// <summary>
    /// Create a result instance with a return value.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="value">Result value.</param>
    /// <param name="nullValueError">Result null value error.</param>
    /// <returns>Returns a result instance.</returns>
    public static Result<TValue> Create<TValue>(TValue? value, Error nullValueError)
            => value is null ? Failure<TValue>(error: nullValueError) : Success(value);

    /// <summary>
    /// Asynchronous create a result instance with a return value.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="valueTask">Asynchronous method with value return.</param>
    /// <param name="nullValueError">Result null value error.</param>
    /// <returns>Returns a result instance.</returns>
    public static async Task<Result<TValue>> Create<TValue>(Task<TValue?> valueTask, Error nullValueError)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        TValue? value = await valueTask.ConfigureAwait(false);
#pragma warning restore CA1062 // Validate arguments of public methods

        if (value is not null)
        {
            return Success(value);
        }

        return Failure<TValue>(error: nullValueError);
    }

    /// <summary>
    /// Create a result instance with a return value.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="either">Either left or right value.</param>
    /// <param name="nullValueError">Result null value error.</param>
    /// <returns>Returns a result instance.</returns>
    public static Result<TValue> Create<TValue>(Either<TValue, Error> either, Error nullValueError)
#pragma warning disable CA1062 // Validate arguments of public methods
        => either
            .Match(
                value => Create(value: value, nullValueError: nullValueError),
                Failure<TValue>);
#pragma warning restore CA1062 // Validate arguments of public methods

    /// <summary>
    /// Asynchronous create a result instance with a return value.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="eitherTask">Asynchronous method with either left or right value.</param>
    /// <param name="nullValueError">Result null value error.</param>
    /// <returns>Returns a result instance.</returns>
    public static async Task<Result<TValue>> Create<TValue>(Task<Either<TValue, Error>> eitherTask, Error nullValueError)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        Either<TValue, Error> either = await eitherTask.ConfigureAwait(false);
#pragma warning restore CA1062 // Validate arguments of public methods

        return Create(either: either, nullValueError: nullValueError);
    }

    /// <summary>
    /// Validate a multiple results.
    /// </summary>
    /// <param name="results">Array of <see cref="Result"/>.</param>
    /// <returns>If one or more of them contain a failure result, it'll return a failure result. Otherwise, it'll return a success result.</returns>
    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        Result firtsFailureResult = Array
            .Find(results, result => result.IsFailure);
        if (firtsFailureResult is not null)
        {
            return firtsFailureResult;
        }

        return Success();
    }

    /// <summary>
    /// Merge a multiple results.
    /// </summary>
    /// <typeparam name="TValue">Result value type.</typeparam>
    /// <param name="results">Array of <see cref="Result{TValue}"/>.</param>
    /// <returns>A combine result instance.</returns>
    public static Result<TValue[]> Combine<TValue>(params Result<TValue>[] results)
    {
        Error[] errors = results
            .Where(result => result.IsFailure)
            .SelectMany(result => result.Errors)
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return Failure<TValue[]>(errors);
        }

        TValue[] values = results
            .Where(result => result.IsSuccess)
            .Select(result => result.Value!)
            .ToArray();

        return Success(values);
    }

    /// <summary>
    /// Merge a multiple results with diferent value types.
    /// </summary>
    /// <typeparam name="T1">Result one value type.</typeparam>
    /// <typeparam name="T2">Result two value type.</typeparam>
    /// <param name="result">Result one.</param>
    /// <param name="result2">Result two.</param>
    /// <returns>Returns a tuple of result values.</returns>
    public static Result<(T1, T2)> Combine<T1, T2>(Result<T1> result, Result<T2> result2)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        if (result.IsFailure)
        {
            return Failure<(T1, T2)>(result.Errors);
        }

        if (result2.IsFailure)
        {
            return Failure<(T1, T2)>(result2.Errors);
        }
#pragma warning restore CA1062 // Validate arguments of public methods

        return Success((result.Value!, result2.Value!));
    }
}

/// <summary>
/// <see cref="Result"/> with a return value.
/// </summary>
/// <typeparam name="TValue">Return value.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    /// Gets the result value.
    /// </summary>
    /// <value>
    /// Contains the result value.
    /// </value>
    public TValue? Value => _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">Result value.</param>
    /// <param name="isSuccess">Result success status.</param>
    /// <param name="error">Result error.</param>
    public Result(TValue value, bool isSuccess, Error? error = null)
        : base(isSuccess, error)
        => _value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">Result value.</param>
    /// <param name="isSuccess">Result success status.</param>
    /// <param name="errors">Result errors.</param>
    protected internal Result(TValue value, bool isSuccess, Error[] errors)
        : base(isSuccess, errors)
        => _value = value;

    /// <summary>
    /// Convert a value to a success result instance.
    /// </summary>
    /// <param name="value">Result value.</param>
#pragma warning disable CA2225 // Operator overloads have named alternates
    public static implicit operator Result<TValue>(TValue value)
#pragma warning restore CA2225 // Operator overloads have named alternates
        => Result.Success(value);
}
