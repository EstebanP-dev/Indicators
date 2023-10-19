using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Domain.Features.MeasurementUnits;

/// <summary>
/// MeasurementUnit repository methods.
/// </summary>
public interface IMeasurementUnitRepository
    : IRepository<MeasurementUnit, int>
{
}
