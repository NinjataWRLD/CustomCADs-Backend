using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Auth.Application.Contracts;

public interface ITokenService
{
    string GenerateRefreshToken();
    AccessTokenDto GenerateAccessToken(UserId accountId, string username, string role);
}
