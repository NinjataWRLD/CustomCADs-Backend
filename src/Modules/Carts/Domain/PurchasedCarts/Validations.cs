namespace CustomCADs.Carts.Domain.PurchasedCarts;

using static PurchasedCartConstants;

public static class Validations
{
	public static PurchasedCart ValidateItems(this PurchasedCart cart)
		=> cart
			.ThrowIfInvalidSize(
				expression: x => x.Items.ToArray(),
				size: (ItemsCountMin, ItemsCountMax),
				property: nameof(cart.Items)
			);
}
