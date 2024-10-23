using CustomCADs.Account.Application.Users.Commands.RenewRefreshToken;
using CustomCADs.Auth.Business.Contracts;
using CustomCADs.Auth.Business.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Wolverine;

namespace CustomCADs.Auth.Business.Managers;

public class TokenManager(IMessageBus bus, IOptions<JwtSettings> jwtOptions) : ITokenManager
{
    private const string Algorithm = SecurityAlgorithms.HmacSha256;
    private const int JwtDurationInMinutes = 15;
    private const int RtDurationInDays = 7;
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

    public async Task<RefreshTokenDto> RenewRefreshTokenAsync(Guid id, CancellationToken ct = default)
    {
        string newRt = GenerateRefreshToken();
        DateTime newEndDate = DateTime.UtcNow.AddDays(RtDurationInDays);

        RenewRefreshTokenCommand command = new(id, newRt, newEndDate);
        await bus.InvokeAsync(command, ct).ConfigureAwait(false);

        return new(newRt, newEndDate);
    }
}
