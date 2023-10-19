using System.Globalization;
using IndicatorsApi.Contracts.Frequencies;
using IndicatorsApi.Contracts.Sections;
using IndicatorsApi.Contracts.SubSections;
using IndicatorsApi.Domain.Features.Frequencies;

namespace IndicatorsApi.Application.Features.Frequencies;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Frequency id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetFrequencyByIdQuery(int Id)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<FrequencyByIdResponse>;

/// <inheritdoc/>
internal sealed class GetFrequencyByIdQueryHandler
    : IQueryHandler<GetFrequencyByIdQuery, FrequencyByIdResponse>
{
    private readonly IFrequencyRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFrequencyByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrequencyRepository"/>.</param>
    public GetFrequencyByIdQueryHandler(IFrequencyRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<FrequencyByIdResponse>> Handle(GetFrequencyByIdQuery request, CancellationToken cancellationToken)
    {
        Frequency? article = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (article == null)
        {
            return DomainErrors.NotFound<Frequency>();
        }

        return article.Adapt<FrequencyByIdResponse>();
    }
}
