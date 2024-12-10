using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public sealed class CreateOrderHandler(IWrites<Order> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateOrderCommand, OrderId>
{
    public async Task<OrderId> Handle(CreateOrderCommand req, CancellationToken ct)
    {
        ShipmentId? shipmentId = null;
        if (req.Delivery)
        {
            CreateShipmentCommand shipmentCommand = new(req.BuyerId);
            shipmentId = await sender.SendCommandAsync(shipmentCommand, ct).ConfigureAwait(false);
        }

        Order order = req.ToOrder(shipmentId);

        await writes.AddAsync(order, ct);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
