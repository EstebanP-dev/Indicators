namespace IndicatorsApi.Contracts.Articles;

/// <summary>
/// Create request.
/// </summary>
/// <param name="Name">Article name.</param>
/// <param name="Description">Article description.</param>
/// <param name="Section">Article section.</param>
/// <param name="SubSection">Article subsection.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class CreateArticleRequest(string Name, string Description, string Section, string SubSection);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter