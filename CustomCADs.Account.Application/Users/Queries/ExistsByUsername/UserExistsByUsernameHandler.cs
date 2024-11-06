using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.ExistsByUsername;

public class UserExistsByUsernameHandler(IUserReads reads)
    : IQueryHandler<UserExistsByUsernameQuery, bool>
{
    public async Task<bool> Handle(UserExistsByUsernameQuery req, CancellationToken ct)
    {
        bool userExists = await reads.ExistsByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false);

        return userExists;
    }
}
