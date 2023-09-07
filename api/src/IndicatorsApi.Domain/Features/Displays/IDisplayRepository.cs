using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Domain.Features.Displays;

/// <summary>
/// Display repository methods.
/// </summary>
public interface IDisplayRepository
    : IRepository<Display, DisplayId>
{
}
