using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.ExistsById;

public class UserExistsByIdHandler(IUserReads reads)
    : IQueryHandler<UserExistsByIdQuery, bool>
{
    public async Task<bool> Handle(UserExistsByIdQuery req, CancellationToken ct)
    {
        bool userExists = await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);

        return userExists;
    }
}
