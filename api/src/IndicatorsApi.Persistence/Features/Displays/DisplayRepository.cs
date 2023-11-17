using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Displays;

/// <inheritdoc cref="IDisplayRepository" />
internal sealed class DisplayRepository
    : Repository<Display, int>, IDisplayRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public DisplayRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
