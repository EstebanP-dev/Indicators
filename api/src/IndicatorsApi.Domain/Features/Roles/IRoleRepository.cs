using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Domain.Features.Roles;

/// <summary>
/// Role repository methods.
/// </summary>
public interface IRoleRepository
    : IRepository<Role, RoleId>
{
}
