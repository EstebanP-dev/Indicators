using IndicatorsApi.Domain.Features.Meanings;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Meanings;

/// <inheritdoc cref="IMeaningRepository" />
internal sealed class MeaningRepository
    : Repository<Meaning, int>, IMeaningRepository
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
