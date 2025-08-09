using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ChangeUsername;

public class ChangeUsernameHandler(IUserService service, IEventRaiser raiser)
	: ICommandHandler<ChangeUsernameCommand>
{
	public async Task Handle(ChangeUsernameCommand req, CancellationToken ct)
	{
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);

		user.SetUsername(req.NewUsername);
		await service.UpdateUsernameAsync(user.Id, user.Username).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new UserEditedApplicationEvent(
				Id: user.AccountId,
				Username: user.Username
			)
		).ConfigureAwait(false);
	}
}
