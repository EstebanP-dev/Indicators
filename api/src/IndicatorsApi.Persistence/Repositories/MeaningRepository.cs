using IndicatorsApi.Domain.Features.Meanings;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class MeaningRepository
    : Repository<Meaning, MeaningId>, IMeaningRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeaningRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public MeaningRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
