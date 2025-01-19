using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase.WithDelivery;

public sealed class PurchaseActiveCartWithDeliveryHandler(IActiveCartReads reads, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
    : ICommandHandler<PurchaseActiveCartWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseActiveCartWithDeliveryCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        if (!cart.HasDelivery)
            throw ActiveCartItemDeliveryException.ById(cart.Id);

        CreatePurchasedCartCommand purchasedCartCommand = new(req.BuyerId);
        var purchasedCartId = await sender.SendCommandAsync(purchasedCartCommand, ct).ConfigureAwait(false);

        GetPurchasedCartByIdQuery purchasedCartQuery = new(purchasedCartId, req.BuyerId);
        var purchasedCart = await sender.SendQueryAsync(purchasedCartQuery, ct).ConfigureAwait(false);
        decimal totalCost = purchasedCart.Total;

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: totalCost,
            description: $"{buyer} bought {cart.TotalCount} products for a total of {totalCost}$.",
            ct
        ).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new ActiveCartPurchasedDomainEvent(
            Id: purchasedCart.Id,
            Items: [.. cart.Items]
        )).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new ActiveCartDeliveryRequestedDomainEvent(
            Id: purchasedCart.Id,
            Weight: cart.TotalDeliveryWeight,
            Count: cart.TotalDeliveryCount,
            ShipmentService: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact
        )).ConfigureAwait(false);

        return message;
    }
}
