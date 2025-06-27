using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;

public class RefreshUserHandler(IUserManager manager, ITokenService service)
	: ICommandHandler<RefreshUserCommand, TokensDto>
{
	public async Task<TokensDto> Handle(RefreshUserCommand req, CancellationToken ct)
	{
		if (string.IsNullOrEmpty(req.Token))
		{
			throw CustomAuthorizationException<User>.Custom("No Refresh Token found.");
		}

		var (User, RefreshToken) = await manager.GetByRefreshTokenAsync(req.Token).ConfigureAwait(false);
		if (User is null || RefreshToken is null)
		{
			throw CustomNotFoundException<User>.ByProp(nameof(RefreshToken), req.Token);
		}

		if (RefreshToken?.ExpiresAt < DateTime.UtcNow)
		{
			throw CustomAuthorizationException<User>.Custom("Refresh Token found, but expired.");
		}

		return await manager.IssueTokens(service, User.Username, longerSession: false).ConfigureAwait(false);
	}
}
