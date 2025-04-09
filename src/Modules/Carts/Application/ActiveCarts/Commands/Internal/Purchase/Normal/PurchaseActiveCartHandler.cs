using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
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

        Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(
            new GetProductPricesByIdsQuery(
                Ids: [.. items.Select(i => i.ProductId)]
            ),
            ct
        ).ConfigureAwait(false);
        decimal totalCost = prices.Sum(p => p.Value);

        string buyer = await sender.SendQueryAsync(
            new GetUsernameByIdQuery(req.BuyerId), 
            ct
        ).ConfigureAwait(false);

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: totalCost,
            description: $"{buyer} bought {items.Length} products for a total of {totalCost}$.",
            ct
        ).ConfigureAwait(false);

        await sender.SendCommandAsync(
            new CreatePurchasedCartCommand(
                BuyerId: req.BuyerId,
                Items: [.. items.Select(x => x.ToDto(buyer))],
                Prices: prices
            ),
            ct
        ).ConfigureAwait(false);

        return message;
    }
}
