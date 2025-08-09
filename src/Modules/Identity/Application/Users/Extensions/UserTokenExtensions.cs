using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users.Entities;

namespace CustomCADs.Identity.Application.Users.Extensions;

public static class UserTokenExtensions
{
	public static async Task<TokensDto> IssueTokens(this IUserService service, ITokenService tokens, User user, bool longerSession)
		=> new(
			Role: user.Role,
			AccessToken: tokens.GenerateAccessToken(
				accountId: user.AccountId,
				username: user.Username,
				role: user.Role
			),
			RefreshToken: (
				await service.AddRefreshTokenAsync(user, tokens.GenerateRefreshToken(), longerSession).ConfigureAwait(false)
			).ToDto(),
			CsrfToken: tokens.GenerateCsrfToken()
		);

	private static TokenDto ToDto(this RefreshToken token)
		=> new(
			Value: token.Value,
			ExpiresAt: token.ExpiresAt
		);
}
