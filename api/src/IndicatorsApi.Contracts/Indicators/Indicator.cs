using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Contracts.Displays;
using IndicatorsApi.Contracts.Frequencies;
using IndicatorsApi.Contracts.Meanings;
using IndicatorsApi.Contracts.MeasurementUnits;
using IndicatorsApi.Contracts.Sources;
using IndicatorsApi.Contracts.Variables;

namespace IndicatorsApi.Contracts.Indicators;

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
/// <summary>
/// Create request.
/// </summary>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
/// <param name="Objective">Indicator objective.</param>
/// <param name="Scope">Indicator scope.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorTypeId">Indicator type id.</param>
/// <param name="MeasurementUnitId">Indicator measurement unit id.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="MeaningId">Indicator meaning id.</param>
/// <param name="FrequencyId">Indicator frecuency id.</param>
/// <param name="Displays">Indicator displays.</param>
/// <param name="Variables">Indicator variables.</param>
/// <param name="Sources">Indicator sources.</param>
/// <param name="Actors">Indicator actors.</param>
public sealed record CreateIndicatorRequest(
    string Code,
    string Name,
    string Objective,
    string Scope,
    string Formula,
    int IndicatorTypeId,
    int MeasurementUnitId,
    string Goal,
    int MeaningId,
    int FrequencyId,
    IEnumerable<int> Displays,
    IEnumerable<CreateVariableIndicatorRequest> Variables,
    IEnumerable<int> Sources,
    IEnumerable<string> Actors);

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">Indicator id.</param>
/// <param name="Code">Indicator code.</param>
/// <param name="Name">Indicator name.</param>
/// <param name="Objective">Indicator objective.</param>
/// <param name="Scope">Indicator scope.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorType">Indicator type.</param>
/// <param name="MeasurementUnit">Indicator measurement unit.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="Meaning">Indicator meaning.</param>
/// <param name="Frequency">Indicator frecuency.</param>
/// <param name="Results">Indicator results.</param>
/// <param name="Displays">Indicator displays.</param>
/// <param name="Variables">Indicator variables.</param>
/// <param name="Sources">Indicator sources.</param>
/// <param name="Actors">Indicator actors.</param>
public sealed record class IndicatorByIdResponse(
    int Id,
    string Code,
    string Name,
    string Objective,
    string Scope,
    string Formula,
    string Goal,
    IndicatorTypePaginationResponse? IndicatorType,
    MeasurementUnitPaginationResponse? MeasurementUnit,
    MeaningPaginationResponse? Meaning,
    FrequencyPaginationResponse? Frequency,
    IEnumerable<IndicatorResultPaginationResponse> Results,
    IEnumerable<DisplayPaginationResponse> Displays,
    IEnumerable<VariableIndicatorPaginationResponse> Variables,
    IEnumerable<SourceByIdResponse> Sources,
    IEnumerable<ActorByIdResponse> Actors);

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
/// <param name="Scope">Indicator scope.</param>
/// <param name="Formula">Indicator formula.</param>
/// <param name="IndicatorTypeId">Indicator type id.</param>
/// <param name="MeasurementUnitId">Indicator measurement unit id.</param>
/// <param name="Goal">Indicator goal.</param>
/// <param name="MeaningId">Indicator meaning id.</param>
/// <param name="FrequencyId">Indicator frecuency id.</param>
/// <param name="Results">Indicator results.</param>
/// <param name="Displays">Indicator displays.</param>
/// <param name="Sources">Indicator sources.</param>
/// <param name="Actors">Indicator actors.</param>
public sealed record class UpdateIndicatorRequest(
    int Id,
    string Code,
    string Name,
    string Objective,
    string Scope,
    string Formula,
    int IndicatorTypeId,
    int MeasurementUnitId,
    string Goal,
    int MeaningId,
    int FrequencyId,
    IEnumerable<int> Results,
    IEnumerable<int> Displays,
    IEnumerable<int> Sources,
    IEnumerable<string> Actors);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter