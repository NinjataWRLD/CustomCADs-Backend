using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.ExistsByUsername;

public class ExistsByUsernameHandler(IUserReads reads)
{
    public async Task<bool> Handle(ExistsByUsernameQuery req, CancellationToken ct)
    {
        bool userExists = await reads.ExistsByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false);

        return userExists;
    }
}
