using IndicatorsApi.Domain.Features.ActorTypes;

namespace IndicatorsApi.Application.Features.ActorTypes.GetActorTypesPagination;

/// <inheritdoc/>
internal sealed class GetActorTypesPaginationQueryHandler
    : IQueryHandler<GetActorTypesPaginationQuery, Pagination<ActorType>>
{
    private readonly IActorTypeRepository _actorTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActorTypesPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="actorTypeRepository">Instance of <see cref="IActorTypeRepository"/>.</param>
    public GetActorTypesPaginationQueryHandler(IActorTypeRepository actorTypeRepository)
    {
        _actorTypeRepository = actorTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<ActorType>>> Handle(GetActorTypesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<ActorType> pagination = await _actorTypeRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: (request.Excludes ?? Array.Empty<int>())
                        .Select(
                            id => ActorTypeId.ToActorTypeId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
