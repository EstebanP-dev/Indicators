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
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.UTF8.GetBytes(_options.SecretKey!);
        SecurityKey securityKey = new SymmetricSecurityKey(key);
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        Claim[] claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Id),
        };

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_options.ExpirationTime),
            Audience = _options.Audience,
            Issuer = _options.Issuer,
            SigningCredentials = signingCredentials,
        };

        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}
