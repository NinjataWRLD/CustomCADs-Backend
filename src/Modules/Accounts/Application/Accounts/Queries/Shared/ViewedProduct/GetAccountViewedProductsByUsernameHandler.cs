using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;

public class GetAccountViewedProductsByUsernameHandler(IAccountReads reads)
	: IQueryHandler<GetAccountViewedProductsByUsernameQuery, ProductId[]>
{
	public async Task<ProductId[]> Handle(GetAccountViewedProductsByUsernameQuery req, CancellationToken ct)
	{
		return await reads.ViewedProductsByUsernameAsync(req.Username, ct).ConfigureAwait(false);
	}
}
