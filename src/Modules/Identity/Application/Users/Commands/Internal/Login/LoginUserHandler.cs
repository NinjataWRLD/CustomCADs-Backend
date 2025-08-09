using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Login;

public class LoginUserHandler(IUserService service, ITokenService tokens)
	: ICommandHandler<LoginUserCommand, TokensDto>
{
	public async Task<TokensDto> Handle(LoginUserCommand req, CancellationToken ct)
	{
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);
		if (!user.Email.IsVerified)
		{
			throw CustomAuthorizationException<User>.Custom($"User: {user.Username} hasn't verified their email.");
		}

		DateTimeOffset? lockoutEnd = await service.GetIsLockedOutAsync(user.Username).ConfigureAwait(false);
		if (lockoutEnd.HasValue)
		{
			TimeSpan timeLeft = lockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
			int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
			throw CustomAuthorizationException<User>.Custom($"The max attempts for logging into Account: {user.Username} has been reached. The account has been locked out for {seconds} seconds.");
		}

		if (!await service.CheckPasswordAsync(user.Username, req.Password).ConfigureAwait(false))
		{
			throw CustomAuthorizationException<User>.Custom($"Account: {user.Username} doesn't exist or password is incorrect.");
		}

		return await service.IssueTokens(tokens, user, req.LongerExpireTime).ConfigureAwait(false);
	}
}
