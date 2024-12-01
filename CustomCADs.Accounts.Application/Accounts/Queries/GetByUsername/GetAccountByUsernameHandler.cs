using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;

namespace CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

public class GetAccountByUsernameHandler(IAccountReads reads)
    : IQueryHandler<GetAccountByUsernameQuery, GetAccountByUsernameDto>
{
    public async Task<GetAccountByUsernameDto> Handle(GetAccountByUsernameQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByUsernameAsync(req.Username, track: false, ct: ct).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ByUsername(req.Username);

        return account.ToGetAccountByUsernameDto();
    }
}
