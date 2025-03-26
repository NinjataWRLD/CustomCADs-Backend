using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.ApplicationEvents.Files;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.EventHandlers.Application;

using static ActiveCartConstants;

public class ProductDeletedHandler(IActiveCartReads reads, IUnitOfWork uow)
{
    public async Task Handle(ProductDeletedApplicationEvent ae)
    {
        int cartsWithItemsForRemovalCount = await reads.CountByProductIdAsync(ae.Id).ConfigureAwait(false);
        if (cartsWithItemsForRemovalCount == 0)
        {
            return;
        }

        if (cartsWithItemsForRemovalCount > BulkDeleteThreshold)
        {
            await uow.BulkDeleteItemsByProductIdAsync(ae.Id).ConfigureAwait(false);
            return;
        }

        ActiveCartQuery query = new(
            Pagination: new(1, cartsWithItemsForRemovalCount),
            ProductId: ae.Id
        );
        Result<ActiveCart> result = await reads.AllAsync(query).ConfigureAwait(false);

        foreach (ActiveCart cart in result.Items)
        {
            cart.RemoveItemsByProductId(ae.Id);
        }

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
