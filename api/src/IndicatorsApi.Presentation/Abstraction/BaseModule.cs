namespace IndicatorsApi.Presentation.Abstraction;

/// <inheritdoc/>
public abstract class BaseModule
    : CarterModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseModule"/> class.
    /// </summary>
    /// <param name="basePath">Base path.</param>
    protected BaseModule(string basePath)
        : base(basePath)
    {
    }

    /// <summary>
    /// Problem response.
    /// </summary>
    /// <param name="errors">List of errors.</param>
    /// <returns>Returns an instance of <see cref="IResult"/>.</returns>
#pragma warning disable CA1002 // Do not expose generic lists
    protected static IResult Problem(List<Error> errors)
#pragma warning restore CA1002 // Do not expose generic lists
    {
        Error firtsError = errors.FirstOrDefault();

        int statusCode = firtsError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Results.Problem(statusCode: statusCode, title: firtsError.Description);
    }

    /// <summary>
    /// Map the result.
    /// </summary>
    /// <typeparam name="TValue">From value type.</typeparam>
    /// <param name="value">Value instance.</param>
    /// <returns>Returns an instance of <see cref="IResult"/>.</returns>
    protected static IResult Result<TValue>(ErrorOr<TValue> value)
    {
        return value
            .Match(
                onValue: Results.Ok,
                onError: Problem);
    }

    /// <summary>
    /// Map the result.
    /// </summary>
    /// <typeparam name="TValue">From value type.</typeparam>
    /// <typeparam name="TReturn">To value type.</typeparam>
    /// <param name="value">Value instance.</param>
    /// <returns>Returns an instance of <see cref="IResult"/>.</returns>
    protected static IResult Result<TValue, TReturn>(ErrorOr<TValue> value)
    {
        return value
            .Match(
                onValue: value => Results.Ok(value!.Adapt<TReturn>()),
                onError: Problem);
    }
}
