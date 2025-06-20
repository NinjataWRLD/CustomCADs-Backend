﻿using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;

public class VerifyUserEmailHandler(IUserManager manager, ITokenService service)
	: ICommandHandler<VerifyUserEmailCommand, TokensDto>
{
	public async Task<TokensDto> Handle(VerifyUserEmailCommand req, CancellationToken ct)
	{
		User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(User.Username), req.Username);

		if (user.Email.IsVerified)
		{
			throw CustomAuthorizationException<User>.Custom($"Account: {user.Username} has already confirmed their email.");
		}

		bool success = await manager.ConfirmEmailAsync(user, req.Token).ConfigureAwait(false);
		if (!success)
		{
			throw CustomAuthorizationException<User>.Custom($"Error confirming Account: {user.Username}'s email.");
		}

		return await manager.IssueTokens(service, req.Username, longerSession: false).ConfigureAwait(false);
	}
}
