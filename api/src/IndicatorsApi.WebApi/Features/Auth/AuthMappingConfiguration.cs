using IndicatorsApi.Application.Features.Auth.Login;
using IndicatorsApi.Contracts.Auth;
using IndicatorsApi.Contracts.Users;
using IndicatorsApi.Domain.Features.Users;

namespace IndicatorsApi.WebApi.Features.Auth;

/// <inheritdoc/>
internal sealed class AuthMappingConfiguration
    : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<LoginRequest, LoginCommand>()
            .Map(dest => dest.Email, src => src.Username)
            .Map(dest => dest.Password, src => src.Password);

        config
            .NewConfig<(string, User), LoginResponse>()
            .Map(dest => dest.Token, src => src.Item1)
            .Map(dest => dest.User, src => src.Item2.Adapt<UserByIdResponse>());
    }
}
