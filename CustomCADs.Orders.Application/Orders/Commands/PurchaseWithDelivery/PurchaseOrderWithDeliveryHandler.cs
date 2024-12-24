using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Orders.Application.Orders.Commands.PurchaseWithDelivery;

public sealed class PurchaseOrderWithDeliveryHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender, IPaymentService payment, IDeliveryService delivery)
    : ICommandHandler<PurchaseOrderWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseOrderWithDeliveryCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.OrderId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.OrderId);

        if (order.BuyerId != req.BuyerId)
            throw OrderAuthorizationException.ByOrderId(order.Id);

        if (order.DesignerId is null)
            throw OrderDesignerException.ById(order.Id);

        if (order.CadId is null)
            throw OrderCadException.ById(order.Id);

        if (!order.Delivery)
            throw OrderDeliveryException.ById(order.Id);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        GetUsernameByIdQuery sellerQuery = new(order.DesignerId.Value);
        string seller = await sender.SendQueryAsync(sellerQuery, ct).ConfigureAwait(false);

        order.SetCompletedStatus();

        int count = 1;
        double weight = req.Weight;
        decimal price = 0m; // integrate order prices

        ShipmentDto shipment = await delivery.ShipAsync(
            req: new(
                Package: "BOX",
                Contents: $"{count} 3D Model/s, each wrapped in a box",
                ParcelCount: count,
                Name: buyer,
                TotalWeight: weight,
                Country: req.Address.Country,
                City: req.Address.City,
                Phone: req.Contact.Phone,
                Email: req.Contact.Email
            ),
            ct: ct
        ).ConfigureAwait(false);
        price += shipment.Price;

        CreateShipmentCommand shipmentCommand = new(
            Address: req.Address,
            BuyerId: req.BuyerId
        );
        ShipmentId shipmentId = await sender.SendCommandAsync(shipmentCommand, ct).ConfigureAwait(false);
        order.SetShipmentId(shipmentId);

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: price,
            description: $"{buyer} bought {order.Name} from {seller}."
        ).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return message;
    }
}
