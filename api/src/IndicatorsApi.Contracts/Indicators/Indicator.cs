namespace IndicatorsApi.Contracts.Indicators;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
/// <param name="Objective">Indicator objective.</param>
/// <param name="Scoped">Indicator scoped.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorTypeId">Indicator type id.</param>
/// <param name="MeasurementUnitId">Indicator measurement unit id.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="MeaningId">Indicator meaning id.</param>
/// <param name="FrequencyId">Indicator frecuency id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateIndicatorRequest(
    string Code,
    string Name,
    string Objective,
    string Scoped,
    string Formula,
    int IndicatorTypeId,
    int MeasurementUnitId,
    string Goal,
    int MeaningId,
    int FrequencyId);

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">Indicator id.</param>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
/// <param name="Objective">Indicator objective.</param>
/// <param name="Scoped">Indicator scoped.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorTypeId">Indicator type id.</param>
/// <param name="MeasurementUnitId">Indicator measurement unit id.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="MeaningId">Indicator meaning id.</param>
/// <param name="FrequencyId">Indicator frecuency id.</param>
public sealed record class IndicatorByIdResponse(
    int Id,
    string Code,
    string Name,
    string Objective,
    string Scoped,
    string Formula,
    int IndicatorTypeId,
    int MeasurementUnitId,
    string Goal,
    int MeaningId,
    int FrequencyId);

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Indicator id.</param>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
public sealed record class IndicatorPaginationResponse(
    int Id,
    string Code,
    string Name);

/// <summary>
/// Update request.
/// </summary>
/// <param name="Id">Indicator id.</param>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
/// <param name="Objective">Indicator objective.</param>
/// <param name="Scoped">Indicator scoped.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorTypeId">Indicator type id.</param>
/// <param name="MeasurementUnitId">Indicator measurement unit id.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="MeaningId">Indicator meaning id.</param>
/// <param name="FrequencyId">Indicator frecuency id.</param>
public sealed record class UpdateIndicatorRequest(
    int Id,
    string Code,
    string Name,
    string Objective,
    string Scoped,
    string Formula,
    int IndicatorTypeId,
    int MeasurementUnitId,
    string Goal,
    int MeaningId,
    int FrequencyId);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter