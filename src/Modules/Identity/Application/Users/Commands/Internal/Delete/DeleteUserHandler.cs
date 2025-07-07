using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Delete;

public class DeleteUserHandler(IUserReads reads, IUserWrites writes, IEventRaiser raiser)
	: ICommandHandler<DeleteUserCommand>
{
	public async Task Handle(DeleteUserCommand req, CancellationToken ct = default)
	{
		User user = await reads.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(req.Username), req.Username);

		await writes.DeleteAsync(req.Username).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new UserDeletedApplicationEvent(user.AccountId)
		).ConfigureAwait(false);
	}
}
