﻿using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Domain.Services;
using Scrutor;

namespace IndicatorsApi.WebApi.Configurations;

/// <summary>
/// Infrastructure configuration.
/// </summary>
public class InfrastructureServiceInstaller
    : IServiceInstaller
{
    /// <inheritdoc/>
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        Infrastructure.InfrastructureAssembly.Assembly,
                        Persistence.PersistenceAssembly.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
    }
}
