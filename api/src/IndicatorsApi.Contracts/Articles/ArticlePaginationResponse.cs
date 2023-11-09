namespace IndicatorsApi.Contracts.Articles;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Article id.</param>
/// <param name="Name">Article name.</param>
/// <param name="Description">Article description.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class ArticlePaginationResponse(string Id, string Name, string Description);
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter