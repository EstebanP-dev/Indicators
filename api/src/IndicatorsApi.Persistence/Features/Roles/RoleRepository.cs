using IndicatorsApi.Domain;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Utils;
using IndicatorsApi.Persistence.Abstractions;

namespace IndicatorsApi.Persistence.Features.Roles;

/// <inheritdoc cref="IRoleRepository" />
internal sealed class RoleRepository
    : Repository<Role, int>, IRoleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRepository"/> class.
    /// </summary>
    /// <param name="context"><see cref="ApplicationDbContext"/> instance.</param>
    public RoleRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
