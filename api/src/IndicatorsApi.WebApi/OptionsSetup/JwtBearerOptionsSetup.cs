using System.Text;
using IndicatorsApi.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IndicatorsApi.WebApi.OptionsSetup;

/// <summary>
/// Setup for <see cref="JwtBearerOptions"/>.
/// </summary>
public class JwtBearerOptionsSetup
    : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtBearerOptionsSetup"/> class.
    /// </summary>
    /// <param name="options">Instance of <see cref="IOptions{JwtOptions}"/>.</param>
    public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        _options = options.Value;
#pragma warning restore CA1062 // Validate arguments of public methods
    }

    /// <inheritdoc/>
    public void Configure(JwtBearerOptions options)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey!)),
        };
#pragma warning restore CA1062 // Validate arguments of public methods
    }
}
