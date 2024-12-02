using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.Orders.Commands.Purchase;

public class PurchaseOrderHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseOrderCommand, string>
{
    public async Task<string> Handle(PurchaseOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.OrderId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.OrderId);

        if (order.BuyerId != req.BuyerId)
            throw OrderAuthorizationException.ByOrderId(order.Id);
        
        if (order.DesignerId is null)
            throw OrderDesignerException.ById(order.Id);
        
        if (order.CadId is null)
            throw OrderCadException.ById(order.Id);
        
        if (order.OrderStatus is not OrderStatus.Finished)
            throw OrderStatusException.ById(order.Id, OrderStatus.Finished);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);
        
        GetUsernameByIdQuery sellerQuery = new(order.DesignerId.Value);
        string seller = await sender.SendQueryAsync(sellerQuery, ct).ConfigureAwait(false);

        order.SetCompletedStatus();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        
        return await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: 0m, // figure it out, idk
            description: $"{buyer} bought {order.Name} from {seller}."
        ).ConfigureAwait(false);
    }
}
