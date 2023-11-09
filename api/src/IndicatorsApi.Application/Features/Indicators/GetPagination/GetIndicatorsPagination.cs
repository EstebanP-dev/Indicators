using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.GetPagination;

/// <summary>
/// Pagation query.
/// </summary>
/// <param name="Parameters">Pagination parameters.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record GetIndicatorsPaginationQuery(PaginationParameters<int> Parameters)
    : IQuery<Pagination<IndicatorPaginationResponse>>;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <inheritdoc/>
internal sealed class GetIndicatorsPaginationQueryHadler
    : IQueryHandler<GetIndicatorsPaginationQuery, Pagination<IndicatorPaginationResponse>>
{
    private readonly IIndicatorRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorsPaginationQueryHadler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorRepository"/>.</param>
    public GetIndicatorsPaginationQueryHadler(IIndicatorRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<IndicatorPaginationResponse>>> Handle(GetIndicatorsPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<IndicatorPaginationResponse> pagination = await _repository
            .GetPaginationAsync(
                parameters: request.Parameters,
                selector: x => new IndicatorPaginationResponse(x.Id, x.Code, x.Name),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return pagination;
    }
}
