using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Logout;

public class LogoutUserHandler(IUserReads reads, IUserWrites writes)
	: ICommandHandler<LogoutUserCommand>
{
	public async Task Handle(LogoutUserCommand req, CancellationToken ct)
	{
		if (string.IsNullOrEmpty(req.RefreshToken))
		{
			throw CustomAuthorizationException<User>.Custom("No Refresh Token found.");
		}

		var (User, Token) = await reads.GetByRefreshTokenAsync(req.RefreshToken).ConfigureAwait(false);
		if (User is null || Token is null)
		{
			throw CustomNotFoundException<User>.ByProp(nameof(req.RefreshToken), req.RefreshToken);
		}

		User.RemoveRefreshToken(Token);
		await writes.UpdateRefreshTokensAsync(
			id: User.Id,
			refreshTokens: [.. User.RefreshTokens]
		).ConfigureAwait(false);
	}
}
