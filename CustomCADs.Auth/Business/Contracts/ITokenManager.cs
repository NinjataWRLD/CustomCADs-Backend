using System.IdentityModel.Tokens.Jwt;

namespace CustomCADs.Auth.Business.Contracts;

public interface ITokenManager
{
    public string GenerateRefreshToken();
    JwtSecurityToken GenerateAccessToken(Guid id, string username, string role);
}
