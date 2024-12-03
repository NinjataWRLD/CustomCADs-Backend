using CustomCADs.Identity.Application.Common.Contracts;
using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Identity.Domain;
using CustomCADs.Identity.Infrastructure.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CustomCADs.Identity.Infrastructure.Services;

using static AuthConstants;

public sealed class AppTokenService(IOptions<JwtSettings> jwtOptions) : ITokenService
{
    private const string Algorithm = SecurityAlgorithms.HmacSha256;
    private readonly JwtSettings jwtSettings = jwtOptions.Value;

    public AccessTokenDto GenerateAccessToken(AccountId accountId, string username, string role)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, accountId.ToString()),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role),
        ];
        SymmetricSecurityKey security = new(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

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
