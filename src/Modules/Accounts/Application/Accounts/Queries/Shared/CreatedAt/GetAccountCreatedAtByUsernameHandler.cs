using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.CreatedAt;

public class GetAccountCreatedAtByUsernameHandler(IAccountReads reads)
    : IQueryHandler<GetAccountCreatedAtByUsernameQuery, DateTimeOffset>
{
    public async Task<DateTimeOffset> Handle(GetAccountCreatedAtByUsernameQuery req, CancellationToken ct = default)
    {
        Account account = await reads.SingleByUsernameAsync(req.Username, track: false, ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Account>.ByProp(nameof(req.Username), req.Username);

        return account.CreatedAt;
    }
}
