using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Identity.Application.Contracts;

public interface ITokenService
{
	string GenerateRefreshToken();
	TokenDto GenerateCsrfToken();
	TokenDto GenerateAccessToken(AccountId accountId, string username, string role);
}
