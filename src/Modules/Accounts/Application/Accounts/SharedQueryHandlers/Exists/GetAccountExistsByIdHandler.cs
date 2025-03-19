using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.Exists;

public class GetAccountExistsByIdHandler(IAccountReads reads)
    : IQueryHandler<GetAccountExistsByIdQuery, bool>
{
    public async Task<bool> Handle(GetAccountExistsByIdQuery req, CancellationToken ct)
    {
        return await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
    }
}
