using IndicatorsApi.Domain.Features.IndicatorTypes;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.IndicatorTypes;

/// <inheritdoc/>
internal sealed class IndicatorTypeRepository
    : Repository<IndicatorType, IndicatorTypeId>, IIndicatorTypeRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorTypeRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public IndicatorTypeRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
