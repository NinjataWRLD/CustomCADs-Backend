using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Catalog;

namespace CustomCADs.Catalog.Application.Products.Events.Application;

public sealed class UserPurchasedProductHandler(IUnitOfWork uow)
{
	public async Task Handle(UserPurchasedProductApplicationEvent req, CancellationToken ct)
	{
		await uow.AddProductPurchasesAsync(req.Ids, count: 1, ct: ct).ConfigureAwait(false);
	}
}
