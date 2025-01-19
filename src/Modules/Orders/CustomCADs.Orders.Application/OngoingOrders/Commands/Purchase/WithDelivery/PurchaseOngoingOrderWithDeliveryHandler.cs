using CustomCADs.Orders.Application.CompletedOrders.Commands.Create;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.Events;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;

public sealed class PurchaseOngoingOrderWithDeliveryHandler(IOngoingOrderReads reads, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
    : ICommandHandler<PurchaseOngoingOrderWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseOngoingOrderWithDeliveryCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.OrderId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.OrderId);

        if (order.BuyerId != req.BuyerId)
            throw OngoingOrderAuthorizationException.ByOrderId(order.Id);

        if (order.OrderStatus is not OngoingOrderStatus.Finished)
            throw OngoingOrderStatusException.NotFinished(order.Id);

        if (!order.Delivery)
            throw OngoingOrderDeliveryException.ById(order.Id);

        if (order.DesignerId is null)
            throw OngoingOrderDesignerException.ById(order.Id);

        if (order.CadId is null)
            throw OngoingOrderCadException.ById(order.Id);

        if (order.Price is null)
            throw OngoingOrderPriceException.ById(order.Id);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId),
            sellerQuery = new(order.DesignerId.Value);

        string[] users = await Task.WhenAll(
            sender.SendQueryAsync(buyerQuery, ct),
            sender.SendQueryAsync(sellerQuery, ct)
        ).ConfigureAwait(false);

        string buyer = users[0], seller = users[1];

        decimal price = order.Price.Value; // integrate order prices
        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: price,
            description: $"{buyer} bought {order.Name} from {seller} for {price}$.",
            ct
        ).ConfigureAwait(false);

        CreateCompletedOrderCommand command = new(
            Name: order.Name,
            Description: order.Description,
            Price: price,
            Delivery: true,
            OrderDate: order.OrderDate,
            BuyerId: order.BuyerId,
            DesignerId: order.DesignerId.Value,
            CadId: order.CadId.Value
        );
        CompletedOrderId completedOrderId = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new OngoingOrderDeliveryRequestedDomainEvent(
            Id: completedOrderId,
            ShipmentService: req.ShipmentService,
            Weight: req.Weight,
            Count: req.Count,
            Address: req.Address,
            Contact: req.Contact
        )).ConfigureAwait(false);

        return message;
    }
}
