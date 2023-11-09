using System;
using Microsoft.AspNetCore.Mvc;

namespace IndicatorsApi.WebApi.Middlewares;

/// <summary>
/// Map the validation error results.
/// </summary>
public sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">Instance of <see cref="RequestDelegate"/>.</param>
    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invoke the request from the user.
    /// </summary>
    /// <param name="context">Instance of <see cref="HttpContext"/>.</param>
    /// <returns>Nothing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        try
        {
            await _next(context)
                .ConfigureAwait(false);
        }
        catch (Application.Validations.ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred",
            };

            if (exception.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exception.Errors;
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response
                .WriteAsJsonAsync(problemDetails)
                .ConfigureAwait(false);
        }
    }
}
