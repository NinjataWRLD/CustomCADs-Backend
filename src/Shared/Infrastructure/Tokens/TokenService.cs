using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CustomCADs.Shared.Infrastructure.Tokens;

using static Constants.Tokens;

public sealed class TokenService(IOptions<JwtSettings> jwtOptions) : ITokenService
{
    private const string Algorithm = SecurityAlgorithms.HmacSha256;
    private readonly JwtSettings jwtSettings = jwtOptions.Value;

    public TokenDto GenerateAccessToken(AccountId accountId, string username, string role)
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

    public TokenDto GenerateRefreshToken(bool longerSession = false)
    {
        byte[] randomNumber = new byte[32];
        RandomNumberGenerator.Fill(randomNumber);

        return new(
            Value: Base64UrlEncoder.Encode(randomNumber),
            ExpiresAt: DateTimeOffset.UtcNow.AddDays(longerSession ? LongerRtDurationInDays : RtDurationInDays)
        );
    }

    public TokenDto GenerateCsrfToken()
    {
        byte[] randomNumber = new byte[32];
        RandomNumberGenerator.Fill(randomNumber);

        return new(
            Value: Convert.ToBase64String(randomNumber),
            ExpiresAt: DateTime.UtcNow.AddMinutes(JwtDurationInMinutes)
        );
    }
}
