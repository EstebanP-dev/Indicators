using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.SubSections;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Articles;

/// <summary>
/// Article model from the database table.
/// </summary>
public sealed class Article
    : Entity<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Article"/> class.
    /// </summary>
    /// <param name="id">Article id.</param>
    public Article(string id)
        : base(id)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Article"/> class.
    /// </summary>
    public Article()
        : base()
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s name.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s name.
    /// </value>
    required public string Name { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s description.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s description.
    /// </value>
    required public string Description { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s <see cref="SectionId"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s <see cref="SectionId"/>.
    /// </value>
    required public string SectionId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Article"/>'s <see cref="SubSectionId"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Article"/>'s <see cref="SubSectionId"/>.
    /// </value>
    required public string SubSectionId { get; set; }

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