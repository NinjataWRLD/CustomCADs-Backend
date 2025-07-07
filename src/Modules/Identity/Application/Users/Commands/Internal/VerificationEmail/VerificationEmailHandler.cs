using CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;

public class VerificationEmailHandler(IUserReads reads, IUserWrites writes, IEventRaiser raiser)
	: ICommandHandler<VerificationEmailCommand>
{
	public async Task Handle(VerificationEmailCommand req, CancellationToken ct)
	{
		User user = await reads.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(User.Username), req.Username);

		string token = await writes.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
		await raiser.RaiseApplicationEventAsync(new EmailVerificationRequestedApplicationEvent(
			Email: user.Email.Value,
			Endpoint: req.GetUri(token)
		)).ConfigureAwait(false);
	}
}
