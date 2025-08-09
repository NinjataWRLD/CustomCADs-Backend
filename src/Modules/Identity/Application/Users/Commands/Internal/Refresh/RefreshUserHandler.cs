using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;

public class RefreshUserHandler(IUserReads reads, IUserWrites writes, ITokenService tokens)
	: ICommandHandler<RefreshUserCommand, TokensDto>
{
	public async Task<TokensDto> Handle(RefreshUserCommand req, CancellationToken ct)
	{
		if (string.IsNullOrEmpty(req.Token))
		{
			throw CustomAuthorizationException<User>.Custom("No Refresh Token found.");
		}

		var (User, RefreshToken) = await reads.GetByRefreshTokenAsync(req.Token).ConfigureAwait(false);
		if (User is null || RefreshToken is null)
		{
			throw CustomNotFoundException<User>.ByProp(nameof(RefreshToken), req.Token);
		}

		if (RefreshToken?.ExpiresAt < DateTime.UtcNow)
		{
			throw CustomAuthorizationException<User>.Custom("Refresh Token found, but expired.");
		}

		return await writes.IssueTokens(tokens, User, longerSession: false).ConfigureAwait(false);
	}
}
