using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Orders.Application.Orders.Commands.PurchaseWithDelivery;

public sealed class PurchaseOrderWithDeliveryHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender, IPaymentService payment)
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

        order.SetCompletedStatus();
        GetUsernameByIdQuery buyerQuery = new(order.BuyerId), 
            sellerQuery = new(order.DesignerId.Value);

        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);
        string seller = await sender.SendQueryAsync(sellerQuery, ct).ConfigureAwait(false);

        int count = 1;
        double weight = req.Weight;
        decimal price = 0m; // integrate order prices

        CreateShipmentCommand shipmentCommand = new(
            Info: new(count, weight, buyer),
            Service: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: req.BuyerId
        );
        var (ShipmentId, Price) = await sender.SendCommandAsync(shipmentCommand, ct).ConfigureAwait(false);

        order.SetShipmentId(ShipmentId);
        price += Price;

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: price,
            description: $"{buyer} bought {order.Name} from {seller}."
        ).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return message;
    }
}
