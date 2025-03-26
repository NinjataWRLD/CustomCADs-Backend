using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;

public class GetAccountViewedProductHandler(IAccountReads reads)
    : IQueryHandler<GetAccountViewedProductQuery, bool>
{
    public async Task<bool> Handle(GetAccountViewedProductQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Account>.ById(req.Id);

        return account.ViewedProductIds.Contains(req.ProductId);
    }
}
