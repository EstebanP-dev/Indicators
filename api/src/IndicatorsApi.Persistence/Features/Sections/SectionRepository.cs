using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Sections;

/// <inheritdoc cref="ISectionRepository" />
internal sealed class SectionRepository
    : Repository<Section, string>, ISectionRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SectionRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public SectionRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}