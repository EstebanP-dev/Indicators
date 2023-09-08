using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Domain.Features.IndicatorTypes;

/// <summary>
/// IndicatorType repository methods.
/// </summary>
public interface IIndicatorTypeRepository
    : IRepository<IndicatorType, IndicatorTypeId>
{
}
