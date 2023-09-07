using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Domain.Features.ActorTypes;

/// <summary>
/// ActorType repository methods.
/// </summary>
public interface IActorTypeRepository
    : IRepository<ActorType, ActorTypeId>
{
}
