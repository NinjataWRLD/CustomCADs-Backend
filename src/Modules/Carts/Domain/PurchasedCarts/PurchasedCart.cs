using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Domain.PurchasedCarts;

public class PurchasedCart : BaseAggregateRoot
{
	private readonly List<PurchasedCartItem> items = [];

	private PurchasedCart() { }
	private PurchasedCart(AccountId buyerId) : this()
	{
		BuyerId = buyerId;
		PurchasedAt = DateTimeOffset.UtcNow;
		PaymentStatus = PaymentStatus.Pending;
	}

	public PurchasedCartId Id { get; init; }
	public PaymentStatus PaymentStatus { get; private set; }
	public DateTimeOffset PurchasedAt { get; }
	public AccountId BuyerId { get; private set; }
	public ShipmentId? ShipmentId { get; private set; }
	public decimal TotalCost => items.Sum(i => i.Cost);
	public bool HasDelivery => items.Any(i => i.ForDelivery);
	public IReadOnlyCollection<PurchasedCartItem> Items => [.. items.OrderByDescending(x => x.AddedAt)];

	public static PurchasedCart Create(AccountId buyerId)
		=> new(buyerId);

	public static PurchasedCart CreateWithId(PurchasedCartId id, AccountId buyerId)
		=> new PurchasedCart(buyerId)
		{
			Id = id
		}
		.ValidateItems();

	public PurchasedCart AddItems(CartItemDto[] items)
	{
		this.items.AddRange([.. items.Select(i => PurchasedCartItem.Create(
			cartId: this.Id,
			productId: i.ProductId,
			cadId: i.CadId,
			customizationId: i.CustomizationId,
			price: i.Price,
			quantity: i.Quantity,
			forDelivery: i.ForDelivery,
			addedAt: i.AddedAt
		))]);
		this.ValidateItems();

		return this;
	}

	public PurchasedCart FinishPayment(bool success = true)
	{
		PaymentStatus = success ? PaymentStatus.Completed : PaymentStatus.Failed;

		return this;
	}

	public PurchasedCart SetShipmentId(ShipmentId shipmentId)
	{
		if (!HasDelivery)
		{
			throw CustomValidationException<PurchasedCart>.Custom("Cannot set ShipmentId on a Purchased Cart with no requested Delivery");
		}
		ShipmentId = shipmentId;

		return this;
	}
}
