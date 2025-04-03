using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Files;

namespace CustomCADs.Carts.Application.ActiveCarts.EventHandlers.Application;

public class ProductDeletedHandler(IUnitOfWork uow)
{
    public async Task Handle(ProductDeletedApplicationEvent ae)
    {
        await uow.BulkDeleteItemsByProductIdAsync(ae.Id).ConfigureAwait(false);
    }
}
