using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Auth.Infrastructure;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CustomCADs.Auth.Application.Services;

using static AuthConstants;

public class AppTokenService(IOptions<JwtSettings> jwtOptions) : ITokenService
{
    private const string Algorithm = SecurityAlgorithms.HmacSha256;
    private readonly JwtSettings jwtSettings = jwtOptions.Value;

    public AccessTokenDto GenerateAccessToken(UserId accountId, string username, string role)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, accountId.ToString()),
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

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return new(jwt, token.ValidTo);
    }

    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        RandomNumberGenerator.Fill(randomNumber);

        string refreshToken = Base64UrlEncoder.Encode(randomNumber);
        return refreshToken;
    }
}
