using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public sealed class PurchaseActiveCartWithDeliveryHandler(IActiveCartReads reads, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
    : ICommandHandler<PurchaseActiveCartWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseActiveCartWithDeliveryCommand req, CancellationToken ct)
    {
        bool hasItems = await reads.ExistsAsync(req.BuyerId, ct).ConfigureAwait(false);
        if (!hasItems) return "";

        ActiveCartItem[] items = await reads.AllAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false);
        if (!items.Any(x => x.ForDelivery))
            throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: false);

        GetProductPricesByIdsQuery pricesQuery = new(
            Ids: [.. items.Select(i => i.ProductId)]
        );
        var prices = await sender.SendQueryAsync(pricesQuery, ct).ConfigureAwait(false);
        prices = prices.ToDictionary(
            x => x.Key,
            x =>
            {
                var item = items.First(i => i.ProductId == x.Key);
                return item.ForDelivery ? x.Value * item.Quantity : x.Value;
            }
        );

        GetCustomizationsCostByIdsQuery costsQuery = new(
            Ids: [..
                items.Where(x => x.ForDelivery && x.CustomizationId is not null)
                    .Select(x => x.CustomizationId!.Value)
            ]
        );
        var costs = await sender.SendQueryAsync(costsQuery, ct).ConfigureAwait(false);
        costs = costs.ToDictionary(
            x => x.Key,
            x =>
            {
                var item = items.First(i => i.CustomizationId == x.Key);
                return x.Value * item.Quantity;
            }
        );

        decimal totalCost = prices.Sum(p => p.Value) + costs.Sum(c => c.Value);

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
            Prices: prices.ToDictionary(
                x => x.Key,
                x =>
                {
                    decimal price = x.Value;
                    var item = items.First(i => i.ProductId == x.Key);

                    return item.ForDelivery && item.CustomizationId is not null
                        ? price + costs[item.CustomizationId.Value]
                        : price;
                }
            )
        );
        var purchasedCartId = await sender.SendCommandAsync(purchasedCartCommand, ct).ConfigureAwait(false);

        GetCustomizationsWeightByIdsQuery weightsQuery = new(
            Ids: [..
                items
                    .Where(x => x.ForDelivery && x.CustomizationId is not null)
                    .Select(x => x.CustomizationId!.Value)
            ]
        );
        var weights = await sender.SendQueryAsync(weightsQuery, ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new ActiveCartDeliveryRequestedDomainEvent(
            Id: purchasedCartId,
            Weight: weights.Sum(x => x.Value),
            Count: items.Count(x => x.ForDelivery),
            ShipmentService: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact
        )).ConfigureAwait(false);

        return message;
    }
}
