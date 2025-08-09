using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;

public class VerifyUserEmailHandler(IUserReads reads, IUserWrites writes, ITokenService tokens)
	: ICommandHandler<VerifyUserEmailCommand, TokensDto>
{
	public async Task<TokensDto> Handle(VerifyUserEmailCommand req, CancellationToken ct)
	{
		User user = await reads.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(User.Username), req.Username);

		if (user.Email.IsVerified)
		{
			throw CustomAuthorizationException<User>.Custom($"Account: {user.Username} has already confirmed their email.");
		}

		bool success = await writes.ConfirmEmailAsync(req.Username, req.Token).ConfigureAwait(false);
		if (!success)
		{
			throw CustomAuthorizationException<User>.Custom($"Error confirming Account: {user.Username}'s email.");
		}

		return await writes.IssueTokens(tokens, user, longerSession: false).ConfigureAwait(false);
	}
}
