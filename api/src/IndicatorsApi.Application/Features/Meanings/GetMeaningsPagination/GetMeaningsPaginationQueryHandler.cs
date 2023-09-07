using IndicatorsApi.Domain.Features.Meanings;

namespace IndicatorsApi.Application.Features.Meanings.GetMeaningsPagination;

/// <inheritdoc/>
internal sealed class GetMeaningsPaginationQueryHandler
    : IQueryHandler<GetMeaningsPaginationQuery, Pagination<Meaning>>
{
    private readonly IMeaningRepository _sourceRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMeaningsPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="IMeaningRepository"/>.</param>
    public GetMeaningsPaginationQueryHandler(IMeaningRepository sourceRepository)
    {
        _sourceRepository = sourceRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<Meaning>>> Handle(GetMeaningsPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<Meaning> pagination = await _sourceRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: (request.Excludes ?? Array.Empty<int>())
                        .Select(
                            id => MeaningId.ToMeaningId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
