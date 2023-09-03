using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Utils;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class SectionRepository
    : Repository<Section, SectionId>, ISectionRepository
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