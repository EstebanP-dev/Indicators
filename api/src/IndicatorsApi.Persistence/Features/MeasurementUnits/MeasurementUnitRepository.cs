using IndicatorsApi.Domain.Features.MeasurementUnits;

namespace IndicatorsApi.Persistence.Features.MeasurementUnits;

/// <inheritdoc cref="IMeasurementUnitRepository" />
internal sealed class MeasurementUnitRepository
    : Repository<MeasurementUnit, int>, IMeasurementUnitRepository
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
