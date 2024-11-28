using CustomCADs.Account.Application.Users.Exceptions;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetUsernameByIdHandler(IUserReads reads)
    : IQueryHandler<GetUsernameByIdQuery, string>
{
    public async Task<string> Handle(GetUsernameByIdQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(req.Id);

        return user.Username;
    }
}
