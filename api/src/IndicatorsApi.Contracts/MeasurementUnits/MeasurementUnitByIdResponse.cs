namespace IndicatorsApi.Contracts.MeasurementUnits;

/// <summary>
/// Gets by id response.
/// </summary>
/// <param name="Id">MeasurementUnit id.</param>
/// <param name="Description">MeasurementUnit description.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class MeasurementUnitByIdResponse(int Id, string Description);
