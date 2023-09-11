using IndicatorsApi.Contracts.Roles;
using IndicatorsApi.Domain.Features.Roles;

namespace IndicatorsApi.WebApi.Features.Roles;

/// <inheritdoc/>
internal sealed class RoleMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, RoleByIdResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<Role, RolePaginationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name);
    }
}
