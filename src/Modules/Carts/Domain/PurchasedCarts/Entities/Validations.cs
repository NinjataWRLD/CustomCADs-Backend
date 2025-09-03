namespace CustomCADs.Carts.Domain.PurchasedCarts.Entities;

using static PurchasedCartConstants.PurchasedCartItems;

public static class Validations
{
	public static PurchasedCartItem ValidateQuantity(this PurchasedCartItem item)
		=> item
			.ThrowIfInvalidRange(
				expression: x => x.Quantity,
				range: (QuantityMin, QuantityMax)
			);

	public static PurchasedCartItem ValidatePrice(this PurchasedCartItem item)
		=> item
			.ThrowIfInvalidRange(
				expression: x => x.Price,
				range: (PriceMin, PriceMax)
			);
}
