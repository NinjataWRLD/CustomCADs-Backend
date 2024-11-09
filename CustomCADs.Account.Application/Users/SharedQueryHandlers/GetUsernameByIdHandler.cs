using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Queries.Users.GetUsernameById;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetUsernameByIdHandler(IUserReads reads)
    : IQueryHandler<GetUsernameByIdQuery, string>
{
    public async Task<string> Handle(GetUsernameByIdQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException(req.Id);

        return user.Username;
    }
}
