using IndicatorsApi.Contracts.Frecuencies;
using IndicatorsApi.Domain.Features.Frecuencies;

namespace IndicatorsApi.Application.Features.Frecuencies;

/// <summary>
/// Gets the pagination query.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetFrecuencyPaginationQuery(PaginationParameters<int> Parameters)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<Pagination<FrecuencyPaginationResponse>>;

/// <inheritdoc/>
internal sealed class GetFrecuencyPaginationQueryHandler
    : IQueryHandler<GetFrecuencyPaginationQuery, Pagination<FrecuencyPaginationResponse>>
{
    private readonly IFrecuencyRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFrecuencyPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IFrecuencyRepository"/>.</param>
    public GetFrecuencyPaginationQueryHandler(IFrecuencyRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<FrecuencyPaginationResponse>>> Handle(GetFrecuencyPaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<FrecuencyPaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new FrecuencyPaginationResponse(
                        x.Id,
                        x.Description),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}