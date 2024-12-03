using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Identity.Application.Common.Contracts;

public interface ITokenService
{
    string GenerateRefreshToken();
    AccessTokenDto GenerateAccessToken(AccountId accountId, string username, string role);
}
