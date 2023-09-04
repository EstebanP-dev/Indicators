using IndicatorsApi.Application.Features.Auth.Login;
using IndicatorsApi.Application.Features.Users.Login;

namespace IndicatorsApi.Persistence.Mapping;

/// <inheritdoc/>
internal sealed class AuthMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginCommand>()
            .Map(dest => dest.Email, src => src.Username)
            .Map(dest => dest.Password, src => src.Password);
    }
}
