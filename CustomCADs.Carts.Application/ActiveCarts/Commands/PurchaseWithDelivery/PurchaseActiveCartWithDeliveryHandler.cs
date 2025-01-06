using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Products.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.PurchaseWithDelivery;

public sealed class PurchaseActiveCartWithDeliveryHandler(IActiveCartReads activeCartsReads, IWrites<PurchasedCart> purchasedCartWrites, IUnitOfWork uow, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseActiveCartWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseActiveCartWithDeliveryCommand req, CancellationToken ct)
    {
        ActiveCart cart = await activeCartsReads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        if (!cart.HasDelivery)
            throw ActiveCartItemDeliveryException.ById(cart.Id);

        var purchasedCart = PurchasedCart.Create(req.BuyerId);
        await purchasedCartWrites.AddAsync(purchasedCart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        ProductId[] productIds = [.. cart.Items.Select(i => i.ProductId)];

        GetProductPricesByIdsQuery pricesQuery = new(productIds);
        Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(pricesQuery, ct).ConfigureAwait(false);

        GetProductCadIdsByIdsQuery cadsCommand = new(productIds);
        Dictionary<ProductId, CadId> cads = await sender.SendQueryAsync(cadsCommand, ct).ConfigureAwait(false);

        purchasedCart.AddItems([.. cart.Items.Select(i =>
            (Price: prices[i.ProductId], Cad: cads[i.ProductId], Item: i)
        )]);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        AddProductPurchaseCommand productPurchasesCommand = new(productIds);
        await sender.SendCommandAsync(productPurchasesCommand, ct).ConfigureAwait(false);

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);
        double weight = cart.TotalDeliveryWeight;
        int deliveryCount = cart.TotalDeliveryCount;

        CreateShipmentCommand shipmentCommand = new(
            Info: new(deliveryCount, weight, buyer),
            Service: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: req.BuyerId
        );
        ShipmentId shipmentId = await sender.SendCommandAsync(shipmentCommand, ct).ConfigureAwait(false);
        purchasedCart.SetShipmentId(shipmentId);

        decimal totalCost = purchasedCart.TotalCost;
        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: totalCost,
            description: $"{buyer} bought {cart.TotalCount} products for a total of {totalCost}$.",
            ct
        ).ConfigureAwait(false);

        return message;
    }
}
