using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Application.Features.Displays.GetDisplaysPagination;

/// <inheritdoc/>
internal sealed class GetDisplaysPaginationQueryHandler
    : IQueryHandler<GetDisplaysPaginationQuery, Pagination<Display>>
{
    private readonly IDisplayRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDisplaysPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IDisplayRepository"/>.</param>
    public GetDisplaysPaginationQueryHandler(IDisplayRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<Display>>> Handle(GetDisplaysPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<Display> pagination = await _repository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: request.Excludes ?? Array.Empty<int>(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
