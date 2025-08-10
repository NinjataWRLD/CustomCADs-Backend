using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;

public class EmailVerificationRequestedEventHandler(IEmailService email)
{
	public async Task Handle(EmailVerificationRequestedApplicationEvent de)
	{
		await email.SendVerificationEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
	}
}
