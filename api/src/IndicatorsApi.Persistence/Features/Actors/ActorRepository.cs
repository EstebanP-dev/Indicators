using IndicatorsApi.Domain.Features.Actors;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Actors;

/// <inheritdoc cref="IActorRepository" />
internal sealed class ActorRepository
    : Repository<Actor, string>, IActorRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActorRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public ActorRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
