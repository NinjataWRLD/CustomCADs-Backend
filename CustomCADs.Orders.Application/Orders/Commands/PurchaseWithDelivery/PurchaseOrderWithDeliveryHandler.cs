using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
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

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId),
            sellerQuery = new(order.DesignerId.Value);

        string[] users = await Task.WhenAll(
            sender.SendQueryAsync(buyerQuery, ct),
            sender.SendQueryAsync(sellerQuery, ct)
        ).ConfigureAwait(false);

        string buyer = users[0], seller = users[1];

        int count = 1;
        double weight = req.Weight;
        CreateShipmentCommand shipmentCommand = new(
            Info: new(count, weight, buyer),
            Service: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: req.BuyerId
        );
        ShipmentId shipmentId = await sender.SendCommandAsync(shipmentCommand, ct).ConfigureAwait(false);
        order.SetShipmentId(shipmentId);

        decimal price = 0m; // integrate order prices
        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: price,
            description: $"{buyer} bought {order.Name} from {seller}.",
            ct
        ).ConfigureAwait(false);

        order.SetCompletedStatus();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return message;
    }
}
