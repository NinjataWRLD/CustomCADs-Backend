using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;

public class GetAccountViewedProductHandler(IAccountReads reads)
	: IQueryHandler<GetAccountViewedProductQuery, bool>
{
	public async Task<bool> Handle(GetAccountViewedProductQuery req, CancellationToken ct)
	{
		ProductId[] viewedProductIds = await reads.ViewedProductsByIdAsync(req.Id, ct).ConfigureAwait(false);
		return viewedProductIds.Contains(req.ProductId);
	}
}
