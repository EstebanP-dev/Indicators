using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Application.Features.Sections.GetSectionById;

/// <inheritdoc/>
internal sealed class GetSectionByIdQueryHandler
    : IQueryHandler<GetSectionByIdQuery, Section>
{
    private readonly ISectionRepository _sectionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSectionByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="sectionRepository">Instance of <see cref="ISectionRepository"/>.</param>
    public GetSectionByIdQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Section>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        Section? section = await _sectionRepository
                .GetByIdAsync(request.Id, cancellationToken)
                .ConfigureAwait(false);

        if (section == null)
        {
            return DomainErrors.NotFound<Section>();
        }

        return section;
    }
}
