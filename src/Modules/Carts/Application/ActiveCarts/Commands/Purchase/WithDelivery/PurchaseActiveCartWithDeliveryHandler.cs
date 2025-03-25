using CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase.WithDelivery;

public sealed class PurchaseActiveCartWithDeliveryHandler(IActiveCartReads reads, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
    : ICommandHandler<PurchaseActiveCartWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseActiveCartWithDeliveryCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCart>.ById(req.BuyerId);
        var items = cart.Items.ToDictionary(x => x.ProductId, x => x);

        int count = cart.TotalCount;
        if (count is 0) return "";

        if (!cart.HasDelivery)
            throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: false);

        GetProductPricesByIdsQuery pricesQuery = new(
            Ids: [.. cart.Items.Select(i => i.ProductId)]
        );
        var prices = await sender.SendQueryAsync(pricesQuery, ct).ConfigureAwait(false);
        prices = prices.ToDictionary(
            x => x.Key,
            x =>
            {
                var item = items[x.Key];
                return item.ForDelivery ? x.Value * item.Quantity : x.Value;
            }
        );

        GetCustomizationsCostByIdsQuery costsQuery = new(
            Ids: [..
                cart.Items
                    .Where(x => x.ForDelivery && x.CustomizationId is not null)
                    .Select(x => x.CustomizationId!.Value)
            ]
        );
        var costs = await sender.SendQueryAsync(costsQuery, ct).ConfigureAwait(false);
        costs = costs.ToDictionary(
            x => x.Key,
            x =>
            {
                var item = cart.Items.First(i => i.CustomizationId == x.Key);
                return item.ForDelivery ? x.Value * item.Quantity : x.Value;
            }
        );

        decimal totalCost = prices.Sum(p => p.Value) + costs.Sum(c => c.Value);

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
            Prices: prices.ToDictionary(
                x => x.Key,
                x =>
                {
                    decimal total = 0m;

                    total += x.Value;
                    var item = items[x.Key];

                    if (item.ForDelivery && item.CustomizationId is not null)
                        total += costs[item.CustomizationId.Value];

                    return total;
                }
            )
        );
        var purchasedCartId = await sender.SendCommandAsync(purchasedCartCommand, ct).ConfigureAwait(false);

        GetCustomizationsWeightByIdsQuery weightsQuery = new(
            Ids: [..
                cart.Items
                    .Where(x => x.ForDelivery && x.CustomizationId is not null)
                    .Select(x => x.CustomizationId!.Value)
            ]
        );
        var weights = await sender.SendQueryAsync(weightsQuery, ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new ActiveCartDeliveryRequestedDomainEvent(
            Id: purchasedCartId,
            Weight: weights.Sum(x => x.Value),
            Count: cart.TotalDeliveryCount,
            ShipmentService: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact
        )).ConfigureAwait(false);

        return message;
    }
}
