using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Domain.PurchasedCarts.Entities;

public class PurchasedCartItem : BaseEntity
{
	private PurchasedCartItem() { }
	private PurchasedCartItem(
		PurchasedCartId cartId,
		ProductId productId,
		CadId cadId,
		CustomizationId? customizationId,
		decimal price,
		int quantity,
		bool forDelivery,
		DateTimeOffset addedAt
	) : this()
	{
		CartId = cartId;
		ProductId = productId;
		CadId = cadId;
		CustomizationId = customizationId;
		Price = price;
		Quantity = quantity;
		ForDelivery = forDelivery;
		AddedAt = addedAt;
	}

	public int Quantity { get; private set; }
	public decimal Price { get; private set; }
	public bool ForDelivery { get; private set; }
	public DateTimeOffset AddedAt { get; private set; }
	public ProductId ProductId { get; }
	public CadId CadId { get; private set; }
	public CustomizationId? CustomizationId { get; }
	public PurchasedCartId CartId { get; }
	public PurchasedCart Cart { get; } = null!;
	public decimal Cost => Price * Quantity;

	public static PurchasedCartItem Create(
		PurchasedCartId cartId,
		ProductId productId,
		CadId cadId,
		CustomizationId? customizationId,
		decimal price,
		int quantity,
		bool forDelivery,
		DateTimeOffset addedAt
	) => new PurchasedCartItem(
			cartId,
			productId,
			cadId,
			customizationId,
			price,
			quantity,
			forDelivery,
			addedAt
		)
		.ValidateQuantity()
		.ValidatePrice();
}
