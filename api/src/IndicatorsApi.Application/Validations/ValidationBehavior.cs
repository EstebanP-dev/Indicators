using FluentValidation.Results;
using IndicatorsApi.Application.Abstraction.Messaging;

namespace IndicatorsApi.Application.Validations;

/// <summary>
/// Inherence of <see cref="IPipelineBehavior{TRequest, TResponse}"/> to validate commands.
/// </summary>
/// <typeparam name="TRequest">Request type.</typeparam>
/// <typeparam name="TResponse">Response type.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">List of validation methods.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Method to handle the behavior.
    /// </summary>
    /// <param name="request">Request value.</param>
    /// <param name="next">Instance of validation.</param>
    /// <param name="cancellationToken">Cancellation token instance.</param>
    /// <returns>Returns a response value.</returns>
    /// <exception cref="ValidationException">Instance of <see cref="ValidationException"/>.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        ValidationContext<TRequest> context = new(request);

        var validations = await Task.WhenAll(_validators
            .Select(validator => validator.ValidateAsync(context: context)))
            .ConfigureAwait(false);

        IEnumerable<Error> errors = (validations ?? Array.Empty<ValidationResult>())
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => Error.Conflict(description: validationFailure.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            throw new ValidationException(errors: errors);
        }

        TResponse response = await next()
            .ConfigureAwait(false);

        return response;
    }
}
