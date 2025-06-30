using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Shared.Abstractions.Tokens;

public interface ITokenService
{
	string GenerateRefreshToken();
	TokenDto GenerateCsrfToken();
	TokenDto GenerateAccessToken(AccountId accountId, string username, string role);
}
