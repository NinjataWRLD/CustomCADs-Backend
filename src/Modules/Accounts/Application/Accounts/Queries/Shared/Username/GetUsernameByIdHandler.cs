using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.Username;

public sealed class GetUsernameByIdHandler(IAccountReads reads)
    : IQueryHandler<GetUsernameByIdQuery, string>
{
    public async Task<string> Handle(GetUsernameByIdQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Account>.ById(req.Id);

        return account.Username;
    }
}
