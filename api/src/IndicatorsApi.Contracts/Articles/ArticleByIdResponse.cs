using IndicatorsApi.Contracts.Sections;
using IndicatorsApi.Contracts.SubSections;

namespace IndicatorsApi.Contracts.Articles;

/// <summary>
/// Gets pagination response.
/// </summary>
/// <param name="Id">Article id.</param>
/// <param name="Name">Article name.</param>
/// <param name="Description">Article description.</param>
/// <param name="Section">Article section.</param>
/// <param name="SubSection">Article subsection.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class ArticleByIdResponse(string Id, string Name, string Description, SectionPaginationResponse? Section, SubSectionPaginationResponse? SubSection);