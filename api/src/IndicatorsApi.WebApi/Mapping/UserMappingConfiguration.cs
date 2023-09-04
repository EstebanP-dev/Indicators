using IndicatorsApi.Contracts.Features.Roles.GetRoleById;
using IndicatorsApi.Contracts.Features.Users.GetUserByEmail;
using IndicatorsApi.Contracts.Features.Users.GetUsersPagination;
using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.Persistence.Mapping;

/// <inheritdoc/>
internal sealed class UserMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserByEmailResponse>()
            .Map(dest => dest.Email, src => src.Id.Value)
            .Map(dest => dest.Roles, src => src.Roles)
            .ConstructUsing(src => new UserByEmailResponse(src.Id.Value, src.Roles.Adapt<IEnumerable<RoleByIdResponse>>()));

        config.NewConfig<User, UserPaginationResponse>()
            .Map(dest => dest.Email, src => src.Id.Value);
    }
}
