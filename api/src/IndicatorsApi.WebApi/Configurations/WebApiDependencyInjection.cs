using System.Reflection;

namespace IndicatorsApi.WebApi.Configurations;

/// <summary>
/// WebApi dependency injection.
/// </summary>
public static class WebApiDependencyInjection
{
    /// <summary>
    /// Install services.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">Instance of <see cref="IConfiguration"/>.</param>
    /// <param name="assemblies">Assembly layers.</param>
    /// <returns>Returns an instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection InstallServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        IEnumerable<IServiceInstaller> serviceInstallers = assemblies
            .SelectMany(assemblies => assemblies.DefinedTypes)
            .Where(IsAssignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (IServiceInstaller serviceInstaller in serviceInstallers)
        {
            serviceInstaller.Install(services, configuration);
        }

        return services;
    }

    private static bool IsAssignableToType<T>(TypeInfo typeInfo)
    {
        return typeof(T)
            .IsAssignableFrom(typeInfo)
            && !typeInfo.IsInterface
            && !typeInfo.IsAbstract;
    }
}
