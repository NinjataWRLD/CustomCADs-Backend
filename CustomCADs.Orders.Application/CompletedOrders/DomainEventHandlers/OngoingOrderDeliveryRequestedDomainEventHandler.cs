using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Orders.Domain.OngoingOrders.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Orders.Application.CompletedOrders.DomainEventHandlers;

public class OngoingOrderDeliveryRequestedDomainEventHandler(ICompletedOrderReads reads, IRequestSender sender)
{
    public async Task Handle(OngoingOrderDeliveryRequestedDomainEvent de)
    {
        CompletedOrder order = await reads.SingleByIdAsync(de.Id, track: false).ConfigureAwait(false)
            ?? throw CompletedOrderNotFoundException.ById(de.Id);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery).ConfigureAwait(false);
        int count = de.Count;
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
    }
}
