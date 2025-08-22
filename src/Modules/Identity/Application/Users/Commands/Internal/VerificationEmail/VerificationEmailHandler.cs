using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;
using CustomCADs.Shared.Application.Abstractions.Events;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;

public class VerificationEmailHandler(IUserService service, IEventRaiser raiser, IOptions<ClientUrlSettings> settings)
	: ICommandHandler<VerificationEmailCommand>
{
	private readonly string clientUrl = settings.Value.Preferred;

	public async Task Handle(VerificationEmailCommand req, CancellationToken ct)
	{
		string token = await service.GenerateEmailConfirmationTokenAsync(req.Username).ConfigureAwait(false);
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new EmailVerificationRequestedApplicationEvent(
			Email: user.Email.Value,
			Endpoint: GetConfirmEmailPage(req.Username, token)
		)).ConfigureAwait(false);
	}

	private string GetConfirmEmailPage(string username, string token)
		=> $"{clientUrl}/confirm-email?username={username}&token={token}";
}
