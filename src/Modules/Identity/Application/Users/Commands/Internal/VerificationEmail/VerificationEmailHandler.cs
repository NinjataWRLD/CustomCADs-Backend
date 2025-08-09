using CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;
using CustomCADs.Shared.Abstractions.Events;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;

public class VerificationEmailHandler(IUserService service, IEventRaiser raiser)
	: ICommandHandler<VerificationEmailCommand>
{
	public async Task Handle(VerificationEmailCommand req, CancellationToken ct)
	{
		string token = await service.GenerateEmailConfirmationTokenAsync(req.Username).ConfigureAwait(false);
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new EmailVerificationRequestedApplicationEvent(
			Email: user.Email.Value,
			Endpoint: req.GetUri(token)
		)).ConfigureAwait(false);
	}
}
