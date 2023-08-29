using IndicatorsApi.Domain.Features.Sections;

namespace IndicatorsApi.Application.Features.Sections.GetSectionById;

/// <inheritdoc/>
internal sealed class GetSectionByIdQueryHandler
    : IQueryHandler<GetSectionByIdQuery, SectionResponse>
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
    public async Task<Result<SectionResponse>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        Either<Section, Error> either = await _sectionRepository
                .GetByIdAsync(request.Id, cancellationToken)
                .ConfigureAwait(false);

        return either
            .Match(
                left: MapSectionResponse,
                right: MapErrorResponse);
    }

    private Result<SectionResponse> MapErrorResponse(Error error)
    {
       return Result.Failure<SectionResponse>(error);
    }

    private Result<SectionResponse> MapSectionResponse(Section section)
    {
        return section.Adapt<SectionResponse>();
    }
}
