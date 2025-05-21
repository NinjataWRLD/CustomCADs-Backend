using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Delete;

public class DeleteAccountHandler(IUserManager manager, IEventRaiser raiser)
    : ICommandHandler<DeleteAccountCommand>
{
    public async Task Handle(DeleteAccountCommand req, CancellationToken ct = default)
    {
        User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ByProp(nameof(req.Username), req.Username);

        await manager.DeleteAsync(req.Username).ConfigureAwait(false);

        await raiser.RaiseApplicationEventAsync(
            new UserDeletedApplicationEvent(user.AccountId)
        ).ConfigureAwait(false);
    }
}