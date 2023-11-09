using System.Globalization;
using IndicatorsApi.Contracts.Articles;
using IndicatorsApi.Contracts.Sections;
using IndicatorsApi.Contracts.SubSections;
using IndicatorsApi.Domain.Features.Articles;

namespace IndicatorsApi.Application.Features.Articles;

/// <summary>
/// Get By Id Query.
/// </summary>
/// <param name="Id">Article id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class GetArticleByIdQuery(string Id)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    : IQuery<ArticleByIdResponse>;

/// <inheritdoc/>
internal sealed class GetArticleByIdQueryHandler
    : IQueryHandler<GetArticleByIdQuery, ArticleByIdResponse>
{
    private readonly IArticleRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetArticleByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IArticleRepository"/>.</param>
    public GetArticleByIdQueryHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<ArticleByIdResponse>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        Article? article = await _repository
            .GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (article == null)
        {
            return ArticleErrors.NotFound;
        }

        return article.Adapt<ArticleByIdResponse>();
    }
}
