# Base Module

En el modulo base, creamos algunos comportamientos adicionales, protegidos (Solo la clase y las que hijas, pueden usarlos).

```csharp
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
        int[] ids = excludes
            .Where(ex => int.TryParse(ex, out var intExclude))
            .Select(ex => int.Parse(ex))
            .ToArray();
        return ids;
    }
}
```
