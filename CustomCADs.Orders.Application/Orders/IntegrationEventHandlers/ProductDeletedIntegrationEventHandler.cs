using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.IntegrationEvents.Inventory;

namespace CustomCADs.Orders.Application.Orders.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandler(IOrderReads reads, IWrites<Order> writes, IUnitOfWork uow)
{
    public async Task Handle(ProductDeletedIntegrationEvent _)
    {
        OrderQuery query = new();
        Result<Order> result = await reads.AllAsync(query);

        writes.RemoveRange(result.Items);
        await uow.SaveChangesAsync();
    }
}
