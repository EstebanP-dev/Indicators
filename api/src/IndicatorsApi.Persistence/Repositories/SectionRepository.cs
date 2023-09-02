using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class SectionRepository
    : Repository<Section>, ISectionRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SectionRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public SectionRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<Either<Section, Error>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        Section? section = await SingleByIdAsync(DbContext, id).ConfigureAwait(false);

        return section is not null
            ? new(left: section)
            : new(right: DomainErrors.Section.NotFound(id));
    }

    private static Task<Section?> SingleByIdAsync(ApplicationDbContext context, int id) =>
        context.Sections
            .AsSingleQuery()
            .SingleOrDefaultAsync(x => x.Id == id);
}
