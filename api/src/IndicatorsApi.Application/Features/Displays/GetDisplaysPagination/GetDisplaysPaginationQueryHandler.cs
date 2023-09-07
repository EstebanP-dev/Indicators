using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Application.Features.Displays.GetDisplaysPagination;

/// <inheritdoc/>
internal sealed class GetDisplaysPaginationQueryHandler
    : IQueryHandler<GetDisplaysPaginationQuery, Pagination<Display>>
{
    private readonly IDisplayRepository _displayRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDisplaysPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="displayRepository">Instance of <see cref="IDisplayRepository"/>.</param>
    public GetDisplaysPaginationQueryHandler(IDisplayRepository displayRepository)
    {
        _displayRepository = displayRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<Display>>> Handle(GetDisplaysPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<Display> pagination = await _displayRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: (request.Excludes ?? Array.Empty<int>())
                        .Select(
                            id => DisplayId.ToDisplayId(id))
                        .ToArray(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
