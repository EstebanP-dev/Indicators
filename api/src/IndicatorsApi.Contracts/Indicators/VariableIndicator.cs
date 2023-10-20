using IndicatorsApi.Contracts.Variables;

namespace IndicatorsApi.Contracts.Indicators;

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
/// <summary>
/// Create request.
/// </summary>
/// <param name="Datum">VaribleIndicator datum.</param>
/// <param name="Date">VaribleIndicator date.</param>
/// <param name="UserId">VaribleIndicator user id.</param>
/// <param name="VariableId">VaribleIndicator variable id.</param>
public sealed record CreateVariableIndicatorRequest(
    double Datum,
    DateTime Date,
    string UserId,
    int VariableId);

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">VaribleIndicator id.</param>
/// <param name="Datum">VaribleIndicator datum.</param>
/// <param name="Date">VaribleIndicator date.</param>
/// <param name="UserId">VaribleIndicator user id.</param>
/// <param name="Variable">VaribleIndicator variable.</param>
public sealed record VariableIndicatorPaginationResponse(
    int Id,
    double Datum,
    DateTime Date,
    string UserId,
    VariablePaginationResponse Variable);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter