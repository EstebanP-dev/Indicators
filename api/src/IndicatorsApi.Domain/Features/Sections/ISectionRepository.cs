using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain.Features.Sections;

/// <summary>
/// Section repository methods.
/// </summary>
public interface ISectionRepository
    : IRepository<Section>
{
    /// <summary>
    /// Gets a section by id.
    /// </summary>
    /// <param name="id">Section id.</param>
    /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
    /// <returns>Returns either section or error instance.</returns>
    Task<Either<Section, Error>> GetByIdAsync(int id, CancellationToken cancellationToken);
}
