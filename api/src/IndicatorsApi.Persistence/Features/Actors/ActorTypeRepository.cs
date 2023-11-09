using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Actors;

/// <inheritdoc/>
internal sealed class ActorTypeRepository
    : Repository<ActorType, int>, IActorTypeRepository
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
