using System;
using ErrorOr;
using IndicatorsApi.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IndicatorsApi.WebApi.Middlewares;

/// <summary>
/// Map the exceptions result.
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">Instance of <see cref="RequestDelegate"/>.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
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

#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            await _next(context)
                .ConfigureAwait(false);
        }
        catch (DbUpdateException ex)
        {
            await MapDomainError(DomainErrors.CreationOrUpdatingFailed, context, ex)
                .ConfigureAwait(false);
        }
        catch (OperationCanceledException ex)
        {
            await MapDomainError(DomainErrors.CancelledOperation, context, ex)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await MapDomainError(DomainErrors.UndefinedError, context, ex)
                .ConfigureAwait(false);
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }

    private static async Task MapDomainError(Error error, HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = error.Code,
            Title = "Internal error",
            Detail = error.Description,
        };

        problemDetails.Extensions["errors"] = new
        {
            exception.Source,
            exception.StackTrace,
            exception.Message,
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response
            .WriteAsJsonAsync(problemDetails)
            .ConfigureAwait(false);
    }
}
