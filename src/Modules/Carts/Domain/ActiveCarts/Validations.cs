namespace CustomCADs.Carts.Domain.ActiveCarts;

using static ActiveCartItemConstants;

public static class Validations
{
	public static ActiveCartItem ValidateQuantity(this ActiveCartItem item)
		=> item
			.ThrowIfInvalidRange(
				expression: x => x.Quantity,
				range: (QuantityMin, QuantityMax)
			);
}
