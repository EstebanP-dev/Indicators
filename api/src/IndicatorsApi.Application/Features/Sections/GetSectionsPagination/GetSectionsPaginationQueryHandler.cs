using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Application.Features.Sections.GetSectionsPagination;

/// <inheritdoc/>
internal sealed class GetSectionsPaginationQueryHandler
    : IQueryHandler<GetSectionsPaginationQuery, Pagination<Section>>
{
    private readonly ISectionRepository _sectionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSectionsPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="sectionRepository">Instance of <see cref="ISectionRepository"/>.</param>
    public GetSectionsPaginationQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<Section>>> Handle(GetSectionsPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<Section> pagination = await _sectionRepository
            .GetPaginationAsync(
                page: request.Page,
                rows: request.Rows,
                ids: (request.Excludes ?? Array.Empty<string>())
                    .Select(selector: SectionId.ToSectionId)
                    .ToArray(),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return pagination;
    }
}
