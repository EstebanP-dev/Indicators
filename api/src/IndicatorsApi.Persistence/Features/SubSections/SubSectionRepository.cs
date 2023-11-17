using IndicatorsApi.Domain.Features.SubSections;

namespace IndicatorsApi.Persistence.Features.SubSections;

/// <inheritdoc cref="ISubSectionRepository" />
internal sealed class SubSectionRepository
    : Repository<SubSection, string>, ISubSectionRepository
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