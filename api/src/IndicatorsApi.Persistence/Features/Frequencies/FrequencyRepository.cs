using IndicatorsApi.Domain.Features.Frequencies;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Frequencies;

/// <inheritdoc cref="IFrequencyRepository" />
internal sealed class FrequencyRepository
    : Repository<Frequency, int>, IFrequencyRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrequencyRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public FrequencyRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}