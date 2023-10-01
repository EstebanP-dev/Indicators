using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Articles;

/// <summary>
/// Article model from the database table.
/// </summary>
public sealed class Article
    : Entity<ArticleId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Article"/> class.
    /// </summary>
    /// <param name="id">Article id.</param>
    /// <param name="name">Article name.</param>
    /// <param name="description">Article description.</param>
    /// <param name="sectionId">Article sectionId.</param>
    /// <param name="subsectionId">Article subsectionId.</param>
    public Article(string id, string name, string description, string sectionId, string subsectionId)
        : base(ArticleId.ToArticleId(id))
    {
        Name = name;
        Description = description;
        SectionId = SectionId.ToSectionId(sectionId);
        SubSectionId = SubSectionId.ToSubSectionId(subsectionId);
    }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s description.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s description.
    /// </value>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s <see cref="SectionId"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s <see cref="SectionId"/>.
    /// </value>
    public SectionId SectionId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s <see cref="SubSectionId"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s <see cref="SubSectionId"/>.
    /// </value>
    public SubSectionId SubSectionId { get; set; }

    /// <summary>
    /// Gets the <see cref="Article"/>'s <see cref="Section"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s <see cref="Section"/>.
    /// </value>
    public Section? Section { get; }

    /// <summary>
    /// Gets the <see cref="Article"/>'s <see cref="SubSection"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s <see cref="SubSection"/>.
    /// </value>
    public SubSection? SubSection { get; }
}