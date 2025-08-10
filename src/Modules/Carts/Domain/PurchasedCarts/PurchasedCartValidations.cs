using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.Carts.Domain.PurchasedCarts;

using static PurchasedCartConstants;

public static class PurchasedCartValidations
{
	public static PurchasedCart ValidateItems(this PurchasedCart cart)
	{
		string property = "Items";
		IEnumerable<PurchasedCartItem> items = cart.Items;

		int max = ItemsCountMax, min = ItemsCountMin;
		if (items.Count() > max || items.Count() < min)
		{
			throw CustomValidationException<PurchasedCart>.Range(property, min, max);
		}

		return cart;
	}
}
