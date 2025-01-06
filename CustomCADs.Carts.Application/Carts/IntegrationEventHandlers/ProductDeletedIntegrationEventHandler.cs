using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.Carts.Application.Carts.IntegrationEventHandlers;

using static CartConstants;

public class ProductDeletedIntegrationEventHandler(ICartReads reads, IUnitOfWork uow)
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

        CartQuery query = new(
            Pagination: new(1, cartsWithItemsForRemovalCount),
            ProductId: ie.Id
        );
        Result<Cart> result = await reads.AllAsync(query).ConfigureAwait(false);

        foreach (Cart cart in result.Items)
        {
            cart.RemoveItemsByProductId(ie.Id);
        }

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
