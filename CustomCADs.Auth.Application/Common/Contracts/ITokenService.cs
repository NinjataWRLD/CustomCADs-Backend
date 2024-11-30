using CustomCADs.Auth.Application.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Auth.Application.Common.Contracts;

public interface ITokenService
{
    string GenerateRefreshToken();
    AccessTokenDto GenerateAccessToken(AccountId accountId, string username, string role);
}
