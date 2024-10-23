using CustomCADs.Account.Application.Users.Common.Exceptions;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Users.Commands.RenewRefreshToken;

public class RenewRefreshTokenHandler(IUserReads reads, IUnitOfWork uow)
{
    public async Task Handle(RenewRefreshTokenCommand req, CancellationToken ct = default)
    {
        User user = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException(req.Id);

        user.RefreshToken = new(req.RefreshToken, req.RefreshTokenEndDate);

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
