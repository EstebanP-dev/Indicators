namespace IndicatorsApi.Presentation.Abstraction;

/// <inheritdoc/>
public abstract class BaseModule
    : CarterModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseModule"/> class.
    /// </summary>
    /// <param name="slug">Struct url.</param>
    protected BaseModule(string slug)
        : base($"{Settings.BASEAPI}/{slug}")
    {
        RequireAuthorization();
    }

    /// <summary>
    /// Problem response.
    /// </summary>
    /// <param name="error"><see cref="Error"/> instance.</param>
    /// <returns>Returns an instance of <see cref="IResult"/>.</returns>
    protected static IResult Problem(Error error)
    {
        int statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Results.Problem(statusCode: statusCode, title: error.Description);
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

        return Problem(error: firtsError);
    }

    /// <summary>
    /// Map the result.
    /// </summary>
    /// <typeparam name="TValue">From value type.</typeparam>
    /// <param name="value">Value instance.</param>
    /// <returns>Returns an instance of <see cref="IResult"/>.</returns>
    protected static IResult NoContentResult<TValue>(ErrorOr<TValue> value)
    {
        return value
            .Match(
                onValue: value => Results.NoContent(),
                onError: Problem);
    }

    /// <summary>
    /// Map the result.
    /// </summary>
    /// <typeparam name="TValue">From value type.</typeparam>
    /// <typeparam name="TResponse">To value type.</typeparam>
    /// <param name="value">Value instance.</param>
    /// <returns>Returns an instance of <see cref="IResult"/>.</returns>
    protected static IResult Result<TValue, TResponse>(ErrorOr<TValue> value)
        where TValue : class
    {
        return value
            .Match(
                onValue: value => Results.Ok(value!.Adapt<TResponse>()),
                onError: Problem);
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
                onValue: value => Results.Ok(value!),
                onError: Problem);
    }

    /// <summary>
    /// Gets a list of string from exclude parameter.
    /// </summary>
    /// <param name="exclude">Exclude parameter value.</param>
    /// <returns>Returns a list of string.</returns>
    protected static string[] GetStringsFromExcludeParameter(string? exclude)
    {
        return (exclude ?? string.Empty).Split(";");
    }

    /// <summary>
    /// Gets a list of int from exclude parameter.
    /// </summary>
    /// <param name="exclude">Exclude parameter value.</param>
    /// <returns>Returns a list of int.</returns>
    protected static int[] GetIntsFromExcludeParameter(string? exclude)
    {
        string[] excludes = GetStringsFromExcludeParameter(exclude);
#pragma warning disable CA1305 // Specify IFormatProvider
        int[] ids = excludes
            .Where(ex => int.TryParse(ex, out var intExclude))
            .Select(ex => int.Parse(ex))
            .ToArray();
#pragma warning restore CA1305 // Specify IFormatProvider
        return ids;
    }
}
