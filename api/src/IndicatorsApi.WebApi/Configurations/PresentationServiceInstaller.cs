using Carter;

namespace IndicatorsApi.WebApi.Configurations;

/// <summary>
/// Presentation configuration.
/// </summary>
public class PresentationServiceInstaller
    : IServiceInstaller
{
    /// <inheritdoc/>
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCarter();

        services
            .AddSwaggerGen();
    }
}
