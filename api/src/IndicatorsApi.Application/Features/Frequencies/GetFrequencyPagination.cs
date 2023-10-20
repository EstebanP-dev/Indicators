using IndicatorsApi.Contracts.Frequencies;
using IndicatorsApi.Domain.Features.Frequencies;

namespace IndicatorsApi.Application.Features.Frequencies;

/// <summary>
/// Gets the pagination query.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetFrequencyPaginationQuery(PaginationParameters<int> Parameters)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<Pagination<FrequencyPaginationResponse>>;

/// <inheritdoc/>
internal sealed class GetFrequencyPaginationQueryHandler
    : IQueryHandler<GetFrequencyPaginationQuery, Pagination<FrequencyPaginationResponse>>
{
    private readonly IFrequencyRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFrequencyPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrequencyRepository"/>.</param>
    public GetFrequencyPaginationQueryHandler(IFrequencyRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<FrequencyPaginationResponse>>> Handle(GetFrequencyPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<FrequencyPaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new FrequencyPaginationResponse(
                        x.Id,
                        x.Description),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}