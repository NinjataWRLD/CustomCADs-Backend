using CustomCADs.Shared.Abstractions.Email;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public class PasswordResetRequestedEventHandler(IEmailService service)
{
	public async Task Handle(PasswordResetRequestedApplicationEvent de)
	{
		await service.SendForgotPasswordEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
	}
}
