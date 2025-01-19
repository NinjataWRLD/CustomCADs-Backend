using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.ViewedProduct;

public class GetAccountViewedProductHandler(IAccountReads reads)
    : IQueryHandler<GetAccountViewedProductQuery, bool>
{
    public async Task<bool> Handle(GetAccountViewedProductQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ById(req.Id);

        return account.ViewedProductIds.Contains(req.ProductId);
    }
}
