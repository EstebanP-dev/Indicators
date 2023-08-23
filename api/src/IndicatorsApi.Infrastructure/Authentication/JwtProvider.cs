using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IndicatorsApi.Application.Abstraction;
using IndicatorsApi.Domain.Features.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IndicatorsApi.Infrastructure.Authentication;

/// <inheritdoc/>
internal sealed class JwtProvider
    : IJwtProvider
{
    private readonly JwtOptions _options;

    /// <summary>
    ///  Initializes a new instance of the <see cref="JwtProvider"/> class.
    /// </summary>
    /// <param name="options">Instance of <see cref="IOptions{JwtOptions}"/>.</param>
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc/>
    public string GenerateJwt(User user)
    {
        Claim[] claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Email, user.Email),
        };

        SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!)),
            SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
