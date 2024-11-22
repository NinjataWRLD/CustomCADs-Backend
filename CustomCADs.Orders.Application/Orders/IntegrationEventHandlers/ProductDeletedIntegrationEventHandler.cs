using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Orders.Application.Orders.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandler(IOrderReads reads, IWrites<Order> writes, IUnitOfWork uow)
{
    public async Task Handle(ProductDeletedIntegrationEvent _)
    {
        OrderQuery query = new();
        OrderResult result = await reads.AllAsync(query);

        writes.RemoveRange(result.Orders);
        await uow.SaveChangesAsync();
    }
}
