namespace IndicatorsApi.Domain.Features.Articles;

/// <summary>
/// Article repository methods.
/// </summary>
public interface IArticleRepository
    : IRepository<Article, ArticleId>
{
}