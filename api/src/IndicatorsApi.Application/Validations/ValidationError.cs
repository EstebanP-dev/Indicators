namespace IndicatorsApi.Application.Validations;

/// <summary>
/// Validation error object.
/// </summary>
/// <param name="PropertyName">Name of the property.</param>
/// <param name="ErrorCode">Error code.</param>
/// <param name="ErroMessage">Error message.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record ValidationError(string PropertyName, string ErrorCode, string ErroMessage);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
