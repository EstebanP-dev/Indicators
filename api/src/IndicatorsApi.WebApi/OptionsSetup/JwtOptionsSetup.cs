using IndicatorsApi.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace IndicatorsApi.WebApi.OptionsSetup;

/// <summary>
/// Setup for <see cref="JwtOptions"/>.
/// </summary>
public class JwtOptionsSetup
    : IConfigureOptions<JwtOptions>
{
    private readonly string _sectionName = "Jwt";

    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtOptionsSetup"/> class.
    /// </summary>
    /// <param name="configuration">Instance of <see cref="IConfiguration"/>.</param>
    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Configure the <see cref="JwtOptions"/>.
    /// </summary>
    /// <param name="options">Instance of <see cref="JwtOptions"/>.</param>
    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}
