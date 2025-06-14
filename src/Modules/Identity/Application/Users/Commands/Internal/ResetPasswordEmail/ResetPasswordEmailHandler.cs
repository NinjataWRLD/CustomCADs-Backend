﻿using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Identity.Domain.Users.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

public class ResetPasswordEmailHandler(IUserManager manager, IEventRaiser raiser, IOptions<ClientUrlSettings> settings)
	: ICommandHandler<ResetPasswordEmailCommand>
{
	private readonly string clientUrl = settings.Value.Preferred;

	public async Task Handle(ResetPasswordEmailCommand req, CancellationToken ct)
	{
		User user = await manager.GetByEmailAsync(req.Email).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(User.Email), req.Email);

		string token = await manager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

		await raiser.RaiseDomainEventAsync(new PasswordResetRequestedDomainEvent(
			Email: req.Email,
			Endpoint: GetResetPasswordPage(req.Email, token)
		)).ConfigureAwait(false);
	}

	private string GetResetPasswordPage(string email, string token)
		=> $"{clientUrl}/reset-password?email={email}&token={token}";
}
