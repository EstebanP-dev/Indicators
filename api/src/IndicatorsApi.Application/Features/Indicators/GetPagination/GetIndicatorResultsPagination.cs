using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.GetPagination;

/// <summary>
/// Gets the pagination query.
/// </summary>
/// <param name="Parameters">Pagination parameters.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
public sealed record class GetIndicatorResultsPaginationQuery(PaginationParameters<int> Parameters)
    : IQuery<Pagination<IndicatorResultPaginationResponse>>;

/// <inheritdoc/>
internal sealed class GetIndicatorResultPaginationQueryHandler
    : IQueryHandler<GetIndicatorResultsPaginationQuery, Pagination<IndicatorResultPaginationResponse>>
{
    private readonly IIndicatorResultRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorResultPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorResultRepository"/>.</param>
    public GetIndicatorResultPaginationQueryHandler(IIndicatorResultRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<IndicatorResultPaginationResponse>>> Handle(GetIndicatorResultsPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<IndicatorResultPaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new IndicatorResultPaginationResponse(x.Id, x.Result, x.CalculusDate),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
