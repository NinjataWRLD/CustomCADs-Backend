using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;

public class VerifyUserEmailHandler(IUserService service, ITokenService tokens)
	: ICommandHandler<VerifyUserEmailCommand, TokensDto>
{
	public async Task<TokensDto> Handle(VerifyUserEmailCommand req, CancellationToken ct)
	{
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);

		if (user.Email.IsVerified)
		{
			throw CustomAuthorizationException<User>.Custom($"Account: {user.Username} has already confirmed their email.");
		}
		await service.ConfirmEmailAsync(req.Username, req.Token).ConfigureAwait(false);

		return await service.IssueTokens(tokens, user, longerSession: false).ConfigureAwait(false);
	}
}
