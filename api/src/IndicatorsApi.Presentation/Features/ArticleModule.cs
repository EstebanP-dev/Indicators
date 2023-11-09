using IndicatorsApi.Application.Features.Articles;
using IndicatorsApi.Contracts.Articles;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

/// <inheritdoc/>
public sealed class ArticleModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleModule"/> class.
    /// </summary>
    public ArticleModule()
        : base("articles")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", Create)
            .WithName(nameof(Create))
            .WithTags("Articles");

        app
            .MapGet("/", GetPagination)
            .WithName(nameof(GetPagination))
            .WithTags("Articles");
    }

    private static async Task<IResult> Create(
        [FromBody] CreateArticleRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateArticleCommand command = request.Adapt<CreateArticleCommand>();

        ErrorOr<Created> result = await sender
            .Send(request: command, cancellationToken: cancellationToken)
            .ConfigureAwait(true);

        return Result(value: result);
    }

    private static async Task<IResult> GetPagination(
        [AsParameters] PaginationQueryRequest parameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        string[] ids = GetStringsFromExcludeParameter(exclude: parameters.Exclude);

        GetArticlePaginationQuery query = new(
            new PaginationParameters<string>(
                Page: parameters.Page,
                Rows: parameters.Rows,
                Excludes: ids));

        ErrorOr<Pagination<ArticlePaginationResponse>> result = await sender
            .Send(request: query, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Result(value: result);
    }
}
