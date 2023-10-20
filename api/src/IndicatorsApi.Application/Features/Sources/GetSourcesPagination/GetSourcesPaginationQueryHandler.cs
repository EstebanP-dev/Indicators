using IndicatorsApi.Application.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Application.Features.Sources.GetSourcesPagination;

/// <inheritdoc/>
internal sealed class GetSourcesPaginationQueryHandler
    : IQueryHandler<GetSourcesPaginationQuery, Pagination<Source>>
{
    private readonly ISourceRepository _sourceRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSourcesPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    public GetSourcesPaginationQueryHandler(ISourceRepository sourceRepository)
    {
        _sourceRepository = sourceRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<Source>>> Handle(GetSourcesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<Source> pagination = await _sourceRepository
                .GetPaginationAsync(
                    page: request.Page,
                    rows: request.Rows,
                    ids: request.Excludes ?? Array.Empty<int>(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
