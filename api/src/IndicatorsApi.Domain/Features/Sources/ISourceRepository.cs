using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Domain.Features.Sources;

/// <summary>
/// Source repository methods.
/// </summary>
public interface ISourceRepository
    : IRepository<Source, int>
{
}
