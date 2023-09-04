using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Application.Features.Sections.GetSubSectionsPagination;

/// <inheritdoc/>
internal sealed class GetSubSectionsPaginationQueryHandler
    : IQueryHandler<GetSubSectionsPaginationQuery, Pagination<SubSection>>
{
    private readonly ISubSectionRepository _subSectionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSubSectionsPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="subSectionRepository">Instance of <see cref="ISubSectionRepository"/>.</param>
    public GetSubSectionsPaginationQueryHandler(ISubSectionRepository subSectionRepository)
    {
        _subSectionRepository = subSectionRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<SubSection>>> Handle(GetSubSectionsPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<SubSection> pagination = await _subSectionRepository
            .GetPaginationAsync(
                page: request.Page,
                rows: request.Rows,
                ids: (request.Excludes ?? Array.Empty<string>())
                    .Select(selector: SubSectionId.ToSubSectionId)
                    .ToArray(),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return pagination;
    }
}
