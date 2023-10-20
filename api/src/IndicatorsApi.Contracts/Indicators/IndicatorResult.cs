namespace IndicatorsApi.Contracts.Indicators;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Result">IndicatorResult result.</param>
/// <param name="CalculusDate">IndicatorResult calculus date.</param>
/// <param name="IndicatorId">IndicatorResult indicator id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorResultRequest(double Result, DateTime CalculusDate, int IndicatorId);

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">IndicatorResult id.</param>
/// <param name="Result">IndicatorResult result.</param>
/// <param name="CalculusDate">IndicatorResult calculus date.</param>
/// <param name="Indicator">IndicatorResult indicator.</param>
public sealed record class IndicatorResultByIdResponse(int Id, double Result, DateTime CalculusDate, IndicatorPaginationResponse Indicator);

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">IndicatorResult id.</param>
/// <param name="Result">IndicatorResult result.</param>
/// <param name="CalculusDate">IndicatorResult calculus date.</param>
/// <param name="IndicatorId">IndicatorResult indicator id.</param>
public sealed record class IndicatorResultPaginationResponse(int Id, double Result, DateTime CalculusDate, int IndicatorId);

/// <summary>
/// Update request.
/// </summary>
/// <param name="Id">IndicatorResult id.</param>
/// <param name="Result">IndicatorResult result.</param>
/// <param name="CalculusDate">IndicatorResult calculus date.</param>
/// <param name="IndicatorId">IndicatorResult indicator id.</param>
public sealed record class UpdateIndicatorResultRequest(int Id, double Result, DateTime CalculusDate, int IndicatorId);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter