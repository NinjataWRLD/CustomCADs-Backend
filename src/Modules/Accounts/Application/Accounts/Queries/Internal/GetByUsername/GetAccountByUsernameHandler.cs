using CustomCADs.Accounts.Domain.Repositories.Reads;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetByUsername;

public sealed class GetAccountByUsernameHandler(IAccountReads reads)
    : IQueryHandler<GetAccountByUsernameQuery, GetAccountByUsernameDto>
{
    public async Task<GetAccountByUsernameDto> Handle(GetAccountByUsernameQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByUsernameAsync(req.Username, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Account>.ByProp(nameof(req.Username), req.Username);

        return account.ToGetByUsernameDto();
    }
}
