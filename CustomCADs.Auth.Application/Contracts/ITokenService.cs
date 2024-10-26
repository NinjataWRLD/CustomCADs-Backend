using System.IdentityModel.Tokens.Jwt;

namespace CustomCADs.Auth.Application.Contracts;

public interface ITokenService
{
    public string GenerateRefreshToken();
    JwtSecurityToken GenerateAccessToken(Guid id, string username, string role);
}
