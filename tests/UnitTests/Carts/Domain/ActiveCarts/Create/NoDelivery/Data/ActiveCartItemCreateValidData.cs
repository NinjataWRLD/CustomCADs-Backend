namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.NoDelivery.Data;

using static ActiveCartsData;

public class ActiveCartItemCreateValidData : ActiveCartItemCreateData
{
	public ActiveCartItemCreateValidData()
	{
		Add(ValidBuyerId1, ValidProductId1);
		Add(ValidBuyerId2, ValidProductId2);
	}
}
