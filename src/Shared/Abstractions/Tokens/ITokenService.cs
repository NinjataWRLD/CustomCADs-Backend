using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Shared.Abstractions.Tokens;

public interface ITokenService
{
    TokenDto GenerateRefreshToken(bool longerSession = false);
    TokenDto GenerateAccessToken(AccountId accountId, string username, string role);
}
