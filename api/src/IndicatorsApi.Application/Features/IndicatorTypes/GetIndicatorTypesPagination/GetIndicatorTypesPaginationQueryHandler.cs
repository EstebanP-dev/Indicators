using IndicatorsApi.Domain.Features.IndicatorTypes;

namespace IndicatorsApi.Application.Features.IndicatorTypes.GetIndicatorTypesPagination;

/// <inheritdoc/>
internal sealed class GetIndicatorTypePaginationQueryHandler
    : IQueryHandler<GetIndicatorTypesPaginationQuery, Pagination<IndicatorType>>
{
    private readonly IIndicatorTypeRepository _indicatorTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorTypePaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="indicatorTypeRepository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    public GetIndicatorTypePaginationQueryHandler(IIndicatorTypeRepository indicatorTypeRepository)
    {
        _indicatorTypeRepository = indicatorTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<IndicatorType>>> Handle(GetIndicatorTypesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<IndicatorType> pagination = await _indicatorTypeRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: (request.Excludes ?? Array.Empty<int>())
                        .Select(
                            id => IndicatorTypeId.ToIndicatorTypeId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
