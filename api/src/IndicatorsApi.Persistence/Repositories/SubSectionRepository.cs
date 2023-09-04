using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Utils;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class SubSectionRepository
    : Repository<SubSection, SubSectionId>, ISubSectionRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubSectionRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public SubSectionRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}