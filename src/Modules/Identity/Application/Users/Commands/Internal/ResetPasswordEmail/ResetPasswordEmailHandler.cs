using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;
using CustomCADs.Shared.Application.Abstractions.Events;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

public class ResetPasswordEmailHandler(IUserService service, IEventRaiser raiser, IOptions<ClientUrlSettings> settings)
	: ICommandHandler<ResetPasswordEmailCommand>
{
	private readonly string clientUrl = settings.Value.Preferred;

	public async Task Handle(ResetPasswordEmailCommand req, CancellationToken ct)
	{
		string token = await service.GeneratePasswordResetTokenAsync(req.Email).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new PasswordResetRequestedApplicationEvent(
			Email: req.Email,
			Endpoint: GetResetPasswordPage(req.Email, token)
		)).ConfigureAwait(false);
	}

	private string GetResetPasswordPage(string email, string token)
		=> $"{clientUrl}/reset-password?email={email}&token={token}";
}
