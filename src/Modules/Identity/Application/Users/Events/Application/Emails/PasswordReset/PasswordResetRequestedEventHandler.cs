using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public class PasswordResetRequestedEventHandler(IEmailService email)
{
	public async Task Handle(PasswordResetRequestedApplicationEvent de)
	{
		await email.SendForgotPasswordEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
	}
}
