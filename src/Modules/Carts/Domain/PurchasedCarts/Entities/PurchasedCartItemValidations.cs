namespace CustomCADs.Carts.Domain.PurchasedCarts.Entities;

using static PurchasedCartConstants.PurchasedCartItems;

public static class PurchasedCartItemValidations
{
	public static PurchasedCartItem ValidateQuantity(this PurchasedCartItem item)
	{
		string property = "Quantity";
		int quantity = item.Quantity;

		int max = QuantityMax, min = QuantityMin;
		if (quantity > max || quantity < min)
		{
			throw CustomValidationException<PurchasedCartItem>.Range(property, min, max);
		}

		return item;
	}

	public static PurchasedCartItem ValidatePrice(this PurchasedCartItem item)
	{
		string property = "Price";
		decimal amount = item.Price;

		decimal max = PriceMax, min = PriceMin;
		if (amount > max || amount < min)
		{
			throw CustomValidationException<PurchasedCartItem>.Range(property, min, max);
		}

		return item;
	}
}
