namespace IndicatorsApi.WebApi.Configurations;

/// <summary>
/// Application configuration.
/// </summary>
public class ApplicationServiceInstaller
    : IServiceInstaller
{
    /// <inheritdoc/>
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMediatR(options =>
                options.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

        services
            .AddValidatorsFromAssembly(
                Application.AssemblyReference.Assembly,
                includeInternalTypes: true);
    }
}
