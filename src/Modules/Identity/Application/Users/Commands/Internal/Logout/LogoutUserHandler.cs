using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Logout;

public class LogoutUserHandler(IUserManager manager)
    : ICommandHandler<LogoutUserCommand>
{
    public async Task Handle(LogoutUserCommand req, CancellationToken ct)
    {
        User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ByProp(nameof(User.Username), req.Username);

        user.RefreshToken = null;
        await manager.UpdateAsync(user).ConfigureAwait(false);
    }
}
