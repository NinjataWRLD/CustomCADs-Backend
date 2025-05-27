namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Create.Normal.Data;

using static PurchasedCartsData;

public class PurchasedCartCreateValidData : PurchasedCartCreateData
{
	public PurchasedCartCreateValidData()
	{
		Add(ValidBuyerId1);
		Add(ValidBuyerId2);
	}
}
