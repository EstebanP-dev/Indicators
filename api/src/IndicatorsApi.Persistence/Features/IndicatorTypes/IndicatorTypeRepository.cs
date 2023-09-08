using IndicatorsApi.Domain.Features.ActorTypes;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.IndicatorTypes;

/// <inheritdoc/>
internal sealed class ActorTypeRepository
    : Repository<ActorType, ActorTypeId>, IActorTypeRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActorTypeRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public ActorTypeRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
