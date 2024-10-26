using CustomCADs.Auth.Business.Contracts;
using CustomCADs.Auth.Business.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static CustomCADs.Auth.Infrastructure.AuthConstants;

namespace CustomCADs.Auth.Business.Managers;

public class TokenManager(IOptions<JwtSettings> jwtOptions) : ITokenManager
{
    private const string Algorithm = SecurityAlgorithms.HmacSha256;
    private readonly JwtSettings jwtSettings = jwtOptions.Value;

    public JwtSecurityToken GenerateAccessToken(Guid id, string username, string role)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role),
        ];

        byte[] key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
        SymmetricSecurityKey security = new(key);

        JwtSecurityToken token = new(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(JwtDurationInMinutes),
            signingCredentials: new(security, Algorithm)
        );

        return token;
    }

    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        RandomNumberGenerator.Fill(randomNumber);

        string refreshToken = Base64UrlEncoder.Encode(randomNumber);
        return refreshToken;
    }
}
