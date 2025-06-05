using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

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
