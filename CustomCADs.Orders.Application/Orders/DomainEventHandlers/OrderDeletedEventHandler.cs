using CustomCADs.Orders.Domain.Orders.DomainEvents;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.DomainEventHandlers;

public class OrderDeletedEventHandler(IStorageService storage)
{
    public async Task Handle(OrderDeletedDomainEvent de)
    {
        await storage.DeleteFileAsync(de.ImageKey);
    }
}
