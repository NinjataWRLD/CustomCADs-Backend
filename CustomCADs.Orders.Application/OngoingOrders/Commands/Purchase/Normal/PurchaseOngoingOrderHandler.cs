using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.Normal;

public sealed class PurchaseOngoingOrderHandler(IOngoingOrderReads ongoingOrdersReads, IWrites<CompletedOrder> completedOrderWrites, IUnitOfWork uow, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseOngoingOrderCommand, string>
{
    public async Task<string> Handle(PurchaseOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await ongoingOrdersReads.SingleByIdAsync(req.OrderId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.OrderId);

        if (order.BuyerId != req.BuyerId)
            throw OngoingOrderAuthorizationException.ByOrderId(order.Id);

        if (order.DesignerId is null)
            throw OngoingOrderDesignerException.ById(order.Id);

        if (order.CadId is null)
            throw OngoingOrderCadException.ById(order.Id);

        if (order.Delivery)
            throw OngoingOrderDeliveryException.ById(order.Id);

        CompletedOrder completedOrder = CompletedOrder.Create(
            name: order.Name,
            description: order.Description,
            buyerId: order.BuyerId
        );
        await completedOrderWrites.AddAsync(completedOrder, ct).ConfigureAwait(false);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId),
            sellerQuery = new(order.DesignerId.Value);

        string[] users = await Task.WhenAll(
            sender.SendQueryAsync(buyerQuery, ct),
            sender.SendQueryAsync(sellerQuery, ct)
        ).ConfigureAwait(false);

        string buyer = users[0], seller = users[1];

        decimal price = 0m; // integrate order prices
        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: price,
            description: $"{buyer} bought {order.Name} from {seller}.",
            ct
        ).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return message;
    }
}
