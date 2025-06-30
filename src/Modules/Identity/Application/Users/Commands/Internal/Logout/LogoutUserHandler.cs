using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Logout;

public class LogoutUserHandler(IUserManager manager)
	: ICommandHandler<LogoutUserCommand>
{
	public async Task Handle(LogoutUserCommand req, CancellationToken ct)
	{
		if (string.IsNullOrEmpty(req.RefreshToken))
		{
			throw CustomAuthorizationException<User>.Custom("No Refresh Token found.");
		}

		User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(User.Username), req.Username);

		RefreshToken rt = user.RefreshTokens.FirstOrDefault(rt => rt.Value == req.RefreshToken)
			?? throw CustomAuthorizationException<User>.Custom("Refresh Token not found in User's Refresh Tokens."); ;

		user.RemoveRefreshToken(rt);
		await manager.UpdateAsync(user.Id, user).ConfigureAwait(false);
	}
}
