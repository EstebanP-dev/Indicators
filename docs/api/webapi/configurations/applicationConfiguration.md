# Application Configuration

Esta clase realiza el registro de las clases y servicios de la capa de [Applicación](./../../application/application.md).

```csharp
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
                options.RegisterServicesFromAssembly(Application.ApplicationAssembly.Assembly));

        services.AddValidatorsFromAssembly(
            Application.ApplicationAssembly.Assembly,
            includeInternalTypes: true);
    }
}
```
