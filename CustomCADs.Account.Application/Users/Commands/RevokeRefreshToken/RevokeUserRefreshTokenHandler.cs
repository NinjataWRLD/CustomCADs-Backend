using CustomCADs.Account.Application.Users.Common.Exceptions;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Users.Commands.RevokeRefreshToken;

public class RevokeUserRefreshTokenHandler(IUserReads reads, IUnitOfWork uow)
{
    public async Task Handle(RevokeUserRefreshTokenCommand req, CancellationToken ct = default)
    {
        User user = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException();

        user.RefreshToken = null;

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
