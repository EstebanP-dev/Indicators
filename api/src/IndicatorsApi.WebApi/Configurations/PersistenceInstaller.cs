using IndicatorsApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IndicatorsApi.WebApi.Configurations;

/// <inheritdoc/>
internal sealed class PersistenceInstaller
    : IServiceInstaller
{
    /// <inheritdoc/>
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));
    }
}
