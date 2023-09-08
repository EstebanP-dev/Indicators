using System.Reflection;

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
    }
}
