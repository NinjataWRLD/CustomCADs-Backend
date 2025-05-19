namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.IncreaseQuantity.Data;

public class ActiveCartItemIncreaseQuantityValidData : ActiveCartItemIncreaseQuantityData
{
	public ActiveCartItemIncreaseQuantityValidData()
	{
		Add(1);
		Add(3);
		Add(5);
		Add(10);
	}
}
