using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ChangeUsername;

public class ChangeUsernameHandler(IUserManager manager, IEventRaiser raiser)
	: ICommandHandler<ChangeUsernameCommand>
{
	public async Task Handle(ChangeUsernameCommand req, CancellationToken ct)
	{
		User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(req.Username), req.Username);

		user.SetUsername(req.NewUsername);
		await manager.UpdateUsernameAsync(user.Id, user.Username).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new UserEditedApplicationEvent(
				Id: user.AccountId,
				Username: user.Username,
				Email: user.Email.Value
			)
		).ConfigureAwait(false);
	}
}
