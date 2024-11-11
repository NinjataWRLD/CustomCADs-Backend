using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Auth.Application.Contracts;

public interface ITokenService
{
    string GenerateRefreshToken();
    AccessTokenDto GenerateAccessToken(UserId accountId, string username, string role);
}
