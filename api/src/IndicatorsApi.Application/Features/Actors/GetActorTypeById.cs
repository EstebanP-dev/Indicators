using IndicatorsApi.Contracts.Actors;
using IndicatorsApi.Domain.Features.Actors;

namespace IndicatorsApi.Application.Features.Actors;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">ActorType id.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class")]
public sealed record class GetActorTypeByIdQuery(int Id)
    : IQuery<ActorTypeByIdResponse>;

/// <inheritdoc/>
internal sealed class GetActorTypeByIdQueryHandler
    : IQueryHandler<GetActorTypeByIdQuery, ActorTypeByIdResponse>
{
    private readonly IActorTypeRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActorTypeByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IActorTypeRepository"/>.</param>
    public GetActorTypeByIdQueryHandler(IActorTypeRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<ActorTypeByIdResponse>> Handle(GetActorTypeByIdQuery request, CancellationToken cancellationToken)
    {
        ActorType? actorType = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (actorType is null)
        {
            return DomainErrors.NotFound<ActorType>();
        }

        return actorType.Adapt<ActorTypeByIdResponse>();
    }
}
