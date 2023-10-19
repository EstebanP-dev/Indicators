using IndicatorsApi.Domain.Features.Frecuencies;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Frecuencies;

/// <inheritdoc/>
internal sealed class FrecuencyRepository
    : Repository<Frecuency, int>, IFrecuencyRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrecuencyRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of <see cref="ApplicationDbContext"/>.</param>
    public FrecuencyRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}