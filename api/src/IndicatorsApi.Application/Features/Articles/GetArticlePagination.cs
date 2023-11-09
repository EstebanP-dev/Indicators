using IndicatorsApi.Contracts.Articles;
using IndicatorsApi.Domain.Features.Articles;

namespace IndicatorsApi.Application.Features.Articles;

/// <summary>
/// Gets the pagination query.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetArticlePaginationQuery(PaginationParameters<string> Parameters)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<Pagination<ArticlePaginationResponse>>;

/// <inheritdoc/>
internal sealed class GetArticlePaginationQueryHandler
    : IQueryHandler<GetArticlePaginationQuery, Pagination<ArticlePaginationResponse>>
{
    private readonly IArticleRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetArticlePaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IArticleRepository"/>.</param>
    public GetArticlePaginationQueryHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Pagination<ArticlePaginationResponse>>> Handle(GetArticlePaginationQuery request, CancellationToken cancellationToken)
    {
        Pagination<ArticlePaginationResponse> pagination = await _repository
                .GetPaginationAsync(
                    parameters: request.Parameters,
                    selector: x => new ArticlePaginationResponse(
                        x.Id,
                        x.Name,
                        x.Description),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        return pagination;
    }
}