using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Meanings;

namespace IndicatorsApi.Application.Features.Meanings.GetMeaningById;

/// <inheritdoc/>
internal sealed class GetMeaningByIdQueryHandler
    : IQueryHandler<GetMeaningByIdQuery, Meaning>
{
    private readonly IMeaningRepository _sourceRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMeaningByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="IMeaningRepository"/>.</param>
    public GetMeaningByIdQueryHandler(IMeaningRepository sourceRepository)
    {
        _sourceRepository = sourceRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Meaning>> Handle(GetMeaningByIdQuery request, CancellationToken cancellationToken)
    {
        Meaning? source = await _sourceRepository
            .GetByIdAsync(
                id: MeaningId.ToMeaningId(request.Id),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (source is null)
        {
            return DomainErrors.NotFound<Meaning>();
        }

        return source;
    }
}
