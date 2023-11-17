using IndicatorsApi.Domain.Features.Articles;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Articles;

/// <inheritdoc cref="IArticleRepository" />
internal sealed class ArticleRepository
    : Repository<Article, string>, IArticleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public ArticleRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public override async Task<Article?> GetByIdAsync(string? id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Articles
            .AsNoTracking()
            .Include(article => article.Section)
            .Include(article => article.SubSection)
            .AsSplitQuery()
            .FirstOrDefaultAsync(article => article.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }
}