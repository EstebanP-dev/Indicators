using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Domain.Features.Actors;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Actor id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetActorByIdQuery(string Id)
    : IQuery<ActorByIdResponse>;

/// <inheritdoc/>
internal sealed class GetActorByIdQueryHandler
    : IQueryHandler<GetActorByIdQuery, ActorByIdResponse>
{
    private readonly IActorRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActorByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorRepository"/>.</param>
    public GetActorByIdQueryHandler(IActorRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<ActorByIdResponse>> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
    {
        Actor? actorType = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (actorType is null)
        {
            return DomainErrors.NotFound<Actor>();
        }

        return actorType.Adapt<ActorByIdResponse>();
    }
}
