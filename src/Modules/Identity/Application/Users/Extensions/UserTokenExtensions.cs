using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.Identity.Application.Users.Extensions;

public static class UserTokenExtensions
{
	public static async Task<TokensDto> IssueTokens(this IUserWrites writes, ITokenService tokens, User user, bool longerSession)
		=> new(
			Role: user.Role,
			AccessToken: tokens.GenerateAccessToken(
				accountId: user.AccountId,
				username: user.Username,
				role: user.Role
			),
			RefreshToken: await writes.UpdateRefreshTokenAsync(
				user: user,
				token: tokens.GenerateRefreshToken(),
				longerSession: longerSession
			).ConfigureAwait(false),
			CsrfToken: tokens.GenerateCsrfToken()
		);

	private static async Task<TokenDto> UpdateRefreshTokenAsync(this IUserWrites writes, User user, string token, bool longerSession)
	{
		RefreshToken rt = user.AddRefreshToken(token, longerSession);
		await writes.UpdateRefreshTokensAsync(
			id: user.Id,
			refreshTokens: [.. user.RefreshTokens]
		).ConfigureAwait(false);

		return new(
			Value: rt.Value,
			ExpiresAt: rt.ExpiresAt
		);
	}
}
