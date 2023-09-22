using IndicatorsApi.Domain.Features.MeasurementUnits;

namespace IndicatorsApi.Application.Features.MeasurementUnits.GetMeasurementUnitById;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">MeasurementUnit id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetMeasurementUnitByIdQuery(int Id)
    : IQuery<MeasurementUnit>;
