﻿using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

public sealed class PurchaseActiveCartHandler(IActiveCartReads reads, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseActiveCartCommand, string>
{
    public async Task<string> Handle(PurchaseActiveCartCommand req, CancellationToken ct)
    {
        bool hasItems = await reads.ExistsAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
        if (!hasItems) return "";

        ActiveCartItem[] items = await reads.AllAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false);

        if (items.Any(x => x.ForDelivery))
            throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);

        GetProductPricesByIdsQuery pricesQuery = new(
            Ids: [.. items.Select(i => i.ProductId)]
        );
        Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(pricesQuery, ct).ConfigureAwait(false);
        decimal totalCost = prices.Sum(p => p.Value);

        GetUsernameByIdQuery buyerQuery = new(req.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: totalCost,
            description: $"{buyer} bought {items.Length} products for a total of {totalCost}$.",
            ct
        ).ConfigureAwait(false);

        CreatePurchasedCartCommand purchasedCartCommand = new(
            BuyerId: req.BuyerId,
            Items: [.. items.Select(x => x.ToDto(buyer))],
            Prices: prices
        );
        await sender.SendCommandAsync(purchasedCartCommand, ct).ConfigureAwait(false);

        return message;
    }
}
