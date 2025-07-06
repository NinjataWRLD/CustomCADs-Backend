using CustomCADs.Shared.Abstractions.Email;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;

public class EmailVerificationRequestedEventHandler(IEmailService service)
{
	public async Task Handle(EmailVerificationRequestedApplicationEvent de)
	{
		await service.SendVerificationEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
	}
}
