using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public sealed class PurchaseActiveCartWithDeliveryHandler(IActiveCartReads reads, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
	: ICommandHandler<PurchaseActiveCartWithDeliveryCommand, PaymentDto>
{
	public async Task<PaymentDto> Handle(PurchaseActiveCartWithDeliveryCommand req, CancellationToken ct)
	{
		if (!await reads.ExistsAsync(req.BuyerId, ct).ConfigureAwait(false))
		{
			throw new CustomException("Cart without Items cannot be purchased.");
		}

		ActiveCartItem[] items = await reads.AllAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false);
		if (!items.Any(x => x.ForDelivery))
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: false);
		}

		var prices = await sender.SendQueryAsync(
			new GetProductPricesByIdsQuery(
				Ids: [.. items.Select(i => i.ProductId)]
			),
			ct
		).ConfigureAwait(false);
		prices = prices.ToDictionary(
			x => x.Key,
			x =>
			{
				var item = items.First(i => i.ProductId == x.Key);
				return item.ForDelivery ? x.Value * item.Quantity : x.Value;
			}
		);

		CustomizationId[] customizationIds = [..
			items
				.Where(x => x.ForDelivery && x.CustomizationId is not null)
				.Select(x => x.CustomizationId!.Value)
		];

		var costs = await sender.SendQueryAsync(
			new GetCustomizationsCostByIdsQuery(
				Ids: customizationIds
			),
			ct
		).ConfigureAwait(false);
		costs = costs.ToDictionary(
			x => x.Key,
			x =>
			{
				var item = items.First(i => i.CustomizationId == x.Key);
				return x.Value * item.Quantity;
			}
		);

		decimal totalCost = prices.Sum(p => p.Value) + costs.Sum(c => c.Value);

		string buyer = await sender.SendQueryAsync(
			new GetUsernameByIdQuery(req.BuyerId),
			ct
		).ConfigureAwait(false);

		var purchasedCartId = await sender.SendCommandAsync(
			new CreatePurchasedCartCommand(
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
			),
			ct
		).ConfigureAwait(false);

		var weights = await sender.SendQueryAsync(
			new GetCustomizationsWeightByIdsQuery(
				Ids: customizationIds
			),
			ct
		).ConfigureAwait(false);

		await raiser.RaiseDomainEventAsync(new ActiveCartDeliveryRequestedDomainEvent(
			Id: purchasedCartId,
			Weight: weights.Sum(x => x.Value),
			Count: items.Count(x => x.ForDelivery),
			ShipmentService: req.ShipmentService,
			Address: req.Address,
			Contact: req.Contact
		)).ConfigureAwait(false);

		PaymentDto response = await payment.InitializeCartPayment(
			paymentMethodId: req.PaymentMethodId,
			buyerId: req.BuyerId,
			cartId: purchasedCartId,
			price: totalCost,
			description: $"{buyer} bought {items.Length} products for a total of {totalCost}$.",
			ct
		).ConfigureAwait(false);

		return response;
	}
}
