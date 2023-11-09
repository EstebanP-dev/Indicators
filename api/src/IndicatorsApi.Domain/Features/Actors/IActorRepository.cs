using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Domain.Features.Actors;

/// <summary>
/// Actor repository methods.
/// </summary>
public interface IActorRepository
    : IRepository<Actor, string>
{
}
