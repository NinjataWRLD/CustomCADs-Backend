using CustomCADs.Account.Application.Users.Exceptions;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetTimeZoneByIdHandler(IUserReads reads)
    : IQueryHandler<GetTimeZoneByIdQuery, string>
{
    public async Task<string> Handle(GetTimeZoneByIdQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(req.Id);

        return user.TimeZone;
    }
}
