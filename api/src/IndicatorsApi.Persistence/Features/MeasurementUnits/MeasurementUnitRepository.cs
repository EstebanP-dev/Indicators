using IndicatorsApi.Domain.Features.MeasurementUnits;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.MeasurementUnits;

/// <inheritdoc/>
internal sealed class MeasurementUnitRepository
    : Repository<MeasurementUnit, MeasurementUnitId>, IMeasurementUnitRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeasurementUnitRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public MeasurementUnitRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
