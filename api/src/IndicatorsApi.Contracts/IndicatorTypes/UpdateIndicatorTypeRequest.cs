namespace IndicatorsApi.Contracts.IndicatorTypes;

/// <summary>
/// Update request.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class UpdateIndicatorTypeRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter