using IndicatorsApi.Domain.Features.Displays;

namespace IndicatorsApi.Persistence.Repositories;

/// <inheritdoc/>
internal sealed class DisplayRepository
    : Repository<Display, DisplayId>, IDisplayRepository
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
