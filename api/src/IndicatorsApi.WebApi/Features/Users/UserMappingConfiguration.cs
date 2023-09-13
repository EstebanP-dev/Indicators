using IndicatorsApi.Contracts.Roles;
using IndicatorsApi.Contracts.Users;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.WebApi.Features.Users;

/// <inheritdoc/>
internal sealed class UserMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<User, UserByIdResponse>()
            .Map(dest => dest.Email, src => src.Id.Value)
            .Map(dest => dest.Roles, src => src.Roles)
            .ConstructUsing(src => new UserByIdResponse(src.Id.Value, src.Roles.Adapt<IEnumerable<RolePaginationResponse>>()));

        config.NewConfig<User, UserPaginationResponse>()
            .Map(dest => dest.Email, src => src.Id.Value);
    }
}
