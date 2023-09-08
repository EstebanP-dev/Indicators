namespace IndicatorsApi.Contracts.IndicatorTypes;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorTypeRequest(string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter