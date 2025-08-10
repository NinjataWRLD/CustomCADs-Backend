using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Delete;

public class DeleteUserHandler(IUserService service, IEventRaiser raiser)
	: ICommandHandler<DeleteUserCommand>
{
	public async Task Handle(DeleteUserCommand req, CancellationToken ct = default)
	{
		AccountId accountId = await service.GetAccountIdAsync(req.Username).ConfigureAwait(false);
		await service.DeleteAsync(req.Username).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new UserDeletedApplicationEvent(accountId)
		).ConfigureAwait(false);
	}
}
