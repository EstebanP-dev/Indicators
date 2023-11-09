using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Primitives;
using IndicatorsApi.Domain.Repositories;

namespace IndicatorsApi.Domain.Features.Users;

/// <summary>
/// User repository methods.
/// </summary>
public interface IUserRepository
    : IRepository<User, string>
{
}
