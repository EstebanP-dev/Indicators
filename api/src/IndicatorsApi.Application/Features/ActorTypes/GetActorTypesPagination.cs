using IndicatorsApi.Domain.Features.ActorTypes;

namespace IndicatorsApi.Application.Features.ActorTypes;

/// <summary>
/// Gets the pagination query.
/// </summary>
/// <param name="Page">Page number.</param>
/// <param name="Rows">Page size.</param>
/// <param name="Excludes">Exclude roles ids.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Necessary.")]
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
public sealed record class GetActorTypesPaginationQuery(int Page, int Rows, int[]? Excludes)
    : IQuery<Pagination<ActorType>>;
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly

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