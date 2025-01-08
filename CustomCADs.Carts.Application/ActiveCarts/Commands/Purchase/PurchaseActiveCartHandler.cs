using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Products.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase;

public sealed class PurchaseActiveCartHandler(IActiveCartReads activeCartsReads, IWrites<PurchasedCart> purchasedCartWrites, IUnitOfWork uow, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseActiveCartCommand, string>
{
    public async Task<string> Handle(PurchaseActiveCartCommand req, CancellationToken ct)
    {
        ActiveCart cart = await activeCartsReads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        if (cart.HasDelivery)
            throw ActiveCartItemDeliveryException.ById(cart.Id);

        var purchasedCart = PurchasedCart.Create(req.BuyerId);
        await purchasedCartWrites.AddAsync(purchasedCart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        ProductId[] productIds = [.. cart.Items.Select(i => i.ProductId)];

        GetProductPricesByIdsQuery pricesQuery = new(productIds);
        Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(pricesQuery, ct).ConfigureAwait(false);

        GetProductCadIdsByIdsQuery cadsQuery = new(productIds);
        Dictionary<ProductId, CadId> productCads = await sender.SendQueryAsync(cadsQuery, ct).ConfigureAwait(false);
        
        DuplicateCadsByIdsCommand cadsCommand = new(
            Ids: [.. productCads.Select(c => c.Value)]
        );
        Dictionary<CadId, CadId> itemCads = await sender.SendCommandAsync(cadsCommand, ct).ConfigureAwait(false);

        purchasedCart.AddItems([.. cart.Items.Select(item =>
        {
            decimal price = prices[item.ProductId];
            CadId productCadId = productCads[item.ProductId];
            CadId itemCadId = itemCads[productCadId];
            
            return (price, itemCadId, item);
        })]);

        AddProductPurchaseCommand purchasesCommand = new(productIds);
        await sender.SendCommandAsync(purchasesCommand, ct).ConfigureAwait(false);

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);
        int totalCount = cart.TotalCount;
        decimal totalCost = purchasedCart.TotalCost;

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: totalCost,
            description: $"{buyer} bought {totalCount} products for a total of {totalCost}$.",
            ct
        ).ConfigureAwait(false);
        
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return message;
    }
}
