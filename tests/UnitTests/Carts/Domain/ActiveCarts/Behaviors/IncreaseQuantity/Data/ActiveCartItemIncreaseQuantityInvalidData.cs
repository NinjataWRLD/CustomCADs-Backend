namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.IncreaseQuantity.Data;

using static ActiveCartsData;

public class ActiveCartItemIncreaseQuantityInvalidData : ActiveCartItemIncreaseQuantityData
{
	public ActiveCartItemIncreaseQuantityInvalidData()
	{
		Add(InvalidQuantity);
	}
}
