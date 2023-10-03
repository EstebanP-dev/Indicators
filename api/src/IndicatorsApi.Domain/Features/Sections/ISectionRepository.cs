using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Section repository methods.
/// </summary>
public interface ISectionRepository
    : IRepository<Section, string>
{
}
