using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Files;

namespace CustomCADs.Carts.Application.ActiveCarts.Events.Application.ProductDeleted;

public class ProductDeletedHandler(IUnitOfWork uow)
{
	public async Task Handle(ProductDeletedApplicationEvent ae)
	{
		await uow.BulkDeleteItemsByProductIdAsync(ae.Id).ConfigureAwait(false);
	}
}
