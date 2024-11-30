using CustomCADs.Accounts.Application.Accounts;
using CustomCADs.Accounts.Application.Accounts.Exceptions;
using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;

namespace CustomCADs.Accounts.Application.Accounts.Queries.GetById;

public class GetAccountByIdHandler(IAccountReads reads)
    : IQueryHandler<GetAccountByIdQuery, GetAccountByIdDto>
{
    public async Task<GetAccountByIdDto> Handle(GetAccountByIdQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ById(req.Id);

        return account.ToGetAccountByIdDto();
    }
}
