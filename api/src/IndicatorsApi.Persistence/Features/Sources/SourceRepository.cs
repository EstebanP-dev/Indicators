using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Features.Sources;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Utils;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Sources;

/// <inheritdoc cref="ISourceRepository" />
internal sealed class SourceRepository
    : Repository<Source, int>, ISourceRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SourceRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public SourceRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
