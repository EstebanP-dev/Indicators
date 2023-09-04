using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Section repository methods.
/// </summary>
public interface ISubSectionRepository
    : IRepository<SubSection, SubSectionId>
{
}
