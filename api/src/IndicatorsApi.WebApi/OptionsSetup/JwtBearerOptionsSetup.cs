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
    private readonly JwtOptions _jwtOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtBearerOptionsSetup"/> class.
    /// </summary>
    /// <param name="jwtOptions">Instance of <see cref="IOptions{JwtOptions}"/>.</param>
    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        _jwtOptions = jwtOptions.Value;
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
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey ?? string.Empty)),
        };
#pragma warning restore CA1062 // Validate arguments of public methods
    }
}
