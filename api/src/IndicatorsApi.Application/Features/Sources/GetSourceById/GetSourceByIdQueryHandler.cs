using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Sources;

namespace IndicatorsApi.Application.Features.Sources.GetSourceById;

/// <inheritdoc/>
internal sealed class GetSourceByIdQueryHandler
    : IQueryHandler<GetSourceByIdQuery, Source>
{
    private readonly ISourceRepository _sourceRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSourceByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="sourceRepository">Instance of <see cref="ISourceRepository"/>.</param>
    public GetSourceByIdQueryHandler(ISourceRepository sourceRepository)
    {
        _sourceRepository = sourceRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Source>> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
    {
        Source? source = await _sourceRepository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (source is null)
        {
            return DomainErrors.NotFound<Source>();
        }

        return source;
    }
}
