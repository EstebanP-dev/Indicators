using System.Reflection;
using IndicatorsApi.Application.Features.Users.GetUserByEmail;
using IndicatorsApi.Application.Features.Users.Login;
using IndicatorsApi.Domain.Features.Users;
using Mapster;

namespace IndicatorsApi.WebApi.Configurations;

/// <summary>
/// Presentation configuration.
/// </summary>
public class MapsterServiceInstaller
    : IServiceInstaller
{
    /// <inheritdoc/>
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        TypeAdapterConfig<LoginRequest, LoginCommand>
            .NewConfig()
            .Map(dest => dest.Email, src => src.Username)
            .Map(dest => dest.Password, src => src.Password);
    }
}
