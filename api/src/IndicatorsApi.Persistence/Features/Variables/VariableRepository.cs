using IndicatorsApi.Domain.Features.Variables;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Variables;

/// <inheritdoc/>
internal sealed class VariableRepository
    : Repository<Variable, int>, IVariableRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public VariableRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
