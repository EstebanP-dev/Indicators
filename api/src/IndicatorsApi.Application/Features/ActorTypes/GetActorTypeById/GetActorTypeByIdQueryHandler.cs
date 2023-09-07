using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.ActorTypes;

namespace IndicatorsApi.Application.Features.ActorTypes.GetActorTypeById;

/// <inheritdoc/>
internal sealed class GetActorTypeByIdQueryHandler
    : IQueryHandler<GetActorTypeByIdQuery, ActorType>
{
    private readonly IActorTypeRepository _actorTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActorTypeByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="actorTypeRepository">Instance of <see cref="IActorTypeRepository"/>.</param>
    public GetActorTypeByIdQueryHandler(IActorTypeRepository actorTypeRepository)
    {
        _actorTypeRepository = actorTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<ActorType>> Handle(GetActorTypeByIdQuery request, CancellationToken cancellationToken)
    {
        ActorType? actorType = await _actorTypeRepository
            .GetByIdAsync(
                id: ActorTypeId.ToActorTypeId(request.Id),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (actorType is null)
        {
            return DomainErrors.NotFound<ActorType>();
        }

        return actorType;
    }
}
