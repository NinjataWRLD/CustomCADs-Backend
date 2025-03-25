using CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase.Normal;

public sealed class PurchaseActiveCartHandler(IActiveCartReads reads, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseActiveCartCommand, string>
{
    public async Task<string> Handle(PurchaseActiveCartCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCart>.ById(req.BuyerId);

        int count = cart.TotalCount;
        if (count is 0) return "";

        if (cart.HasDelivery)
            throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);

        GetProductPricesByIdsQuery pricesQuery = new(
            Ids: [.. cart.Items.Select(i => i.ProductId)]
        );
        Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(pricesQuery, ct).ConfigureAwait(false);
        decimal totalCost = prices.Sum(p => p.Value);

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: totalCost,
            description: $"{buyer} bought {count} products for a total of {totalCost}$.",
            ct
        ).ConfigureAwait(false);

        CreatePurchasedCartCommand purchasedCartCommand = new(
            BuyerId: req.BuyerId,
            Items: [.. cart.Items.Select(x => x.ToCartItemDto())],
            Prices: prices
        );
        await sender.SendCommandAsync(purchasedCartCommand, ct).ConfigureAwait(false);

        return message;
    }
}
