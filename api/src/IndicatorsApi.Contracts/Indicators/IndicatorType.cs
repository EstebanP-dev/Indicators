namespace IndicatorsApi.Contracts.Indicators;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">IndicatorType name.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorTypeRequest(string Name);

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
public sealed record class IndicatorTypeByIdResponse(int Id, string Name);

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
public sealed record class IndicatorTypePaginationResponse(int Id, string Name);

/// <summary>
/// Update request.
/// </summary>
/// <param name="Id">IndicatorType id.</param>
/// <param name="Name">IndicatorType name.</param>
public sealed record class UpdateIndicatorTypeRequest(int Id, string Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter