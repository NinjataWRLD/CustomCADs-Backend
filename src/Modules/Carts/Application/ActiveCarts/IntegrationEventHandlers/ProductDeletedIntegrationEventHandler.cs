using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.Carts.Application.ActiveCarts.IntegrationEventHandlers;

using static ActiveCartConstants;

public class ProductDeletedIntegrationEventHandler(IActiveCartReads reads, IUnitOfWork uow)
{
    public async Task Handle(ProductDeletedIntegrationEvent ie)
    {
        int cartsWithItemsForRemovalCount = await reads.CountByProductIdAsync(ie.Id).ConfigureAwait(false);
        if (cartsWithItemsForRemovalCount == 0)
        {
            return;
        }

        if (cartsWithItemsForRemovalCount > BulkDeleteThreshold)
        {
            await uow.BulkDeleteItemsByProductIdAsync(ie.Id).ConfigureAwait(false);
            return;
        }

        ActiveCartQuery query = new(
            Pagination: new(1, cartsWithItemsForRemovalCount),
            ProductId: ie.Id
        );
        Result<ActiveCart> result = await reads.AllAsync(query).ConfigureAwait(false);

        foreach (ActiveCart cart in result.Items)
        {
            cart.RemoveItemsByProductId(ie.Id);
        }

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
