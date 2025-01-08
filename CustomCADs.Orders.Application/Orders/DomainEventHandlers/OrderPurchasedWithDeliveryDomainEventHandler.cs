using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders.Events;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Orders.Application.Orders.DomainEventHandlers;

public class OrderPurchasedWithDeliveryDomainEventHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender)
{
    public async Task Handle(OrderPurchasedWithDeliveryDomainEvent de)
    {
        Order order = await reads.SingleByIdAsync(de.OrderId, track: false).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(de.OrderId);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery).ConfigureAwait(false);
        int count = 1;
        double weight = de.Weight;

        CreateShipmentCommand shipmentCommand = new(
            Info: new(count, weight, buyer),
            Service: de.ShipmentService,
            Address: de.Address,
            Contact: de.Contact,
            BuyerId: order.BuyerId
        );
        ShipmentId shipmentId = await sender.SendCommandAsync(shipmentCommand).ConfigureAwait(false);
        order.SetShipmentId(shipmentId);

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
