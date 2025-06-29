﻿using CustomCADs.Identity.Application.Users.Commands.Internal.ChangeUsername;

namespace CustomCADs.Identity.Endpoints.Identity.Put.ChangeUsername;

public sealed class ChangeUsernameEndpoint(IRequestSender sender)
	: Endpoint<ChangeUsernameRequest>
{
	public override void Configure()
	{
		Put("username");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(d => d
			.WithName(IdentityNames.ChangeUsername)
			.WithSummary("Change Username")
			.WithDescription("Change your Username")
		);
	}

	public override async Task HandleAsync(ChangeUsernameRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new ChangeUsernameCommand(
				Username: User.GetName(),
				NewUsername: req.Username
			),
			ct
		).ConfigureAwait(false);
	}
}
