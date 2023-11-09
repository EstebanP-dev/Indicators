using IndicatorsApi.Contracts.Indicators;
using IndicatorsApi.Domain.Features.Indicators;

namespace IndicatorsApi.Application.Features.Indicators.GetPagination;

/// <summary>
/// Gets the pagination query.
/// </summary>
/// <param name="Parameters">Pagination parameters.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record class.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Necessary.")]
public sealed record class GetIndicatorTypesPaginationQuery(PaginationParameters<int> Parameters)
    : IQuery<Pagination<IndicatorTypePaginationResponse>>;

/// <inheritdoc/>
internal sealed class GetIndicatorTypePaginationQueryHandler
    : IQueryHandler<GetIndicatorTypesPaginationQuery, Pagination<IndicatorTypePaginationResponse>>
{
    private readonly IIndicatorTypeRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIndicatorTypePaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IIndicatorTypeRepository"/>.</param>
    public GetIndicatorTypePaginationQueryHandler(IIndicatorTypeRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<IndicatorTypePaginationResponse>>> Handle(GetIndicatorTypesPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<IndicatorTypePaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new IndicatorTypePaginationResponse(x.Id, x.Name),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}
