using CustomCADs.Auth.Application.Dtos;

namespace CustomCADs.Auth.Application.Contracts;

public interface ITokenService
{
    string GenerateRefreshToken();
    AccessTokenDto GenerateAccessToken(Guid id, Guid accountId, string username, string role);
}
