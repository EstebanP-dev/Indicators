using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Application.Features.Sections.GetSubSectionById;

/// <inheritdoc/>
internal sealed class GetSubSectionByIdQueryHandler
    : IQueryHandler<GetSubSectionByIdQuery, SubSection>
{
    private readonly ISubSectionRepository _subSectionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSubSectionByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="subSectionRepository">Instance of <see cref="ISubSectionRepository"/>.</param>
    public GetSubSectionByIdQueryHandler(ISubSectionRepository subSectionRepository)
    {
        _subSectionRepository = subSectionRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<SubSection>> Handle(GetSubSectionByIdQuery request, CancellationToken cancellationToken)
    {
        SubSection? subSection = await _subSectionRepository
                .GetByIdAsync(SubSectionId.ToSubSectionId(request.Id), cancellationToken)
                .ConfigureAwait(false);

        if (subSection == null)
        {
            return DomainErrors.NotFound<SubSection>();
        }

        return subSection;
    }
}
