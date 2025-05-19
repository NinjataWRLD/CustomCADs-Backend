namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts.Data;

using CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;
using static PurchasedCartsData;

public class CountPurchasedCartsValidData : CountPurchasedCartsData
{
	public CountPurchasedCartsValidData()
	{
		Add(ValidBuyerId1);
		Add(ValidBuyerId2);
	}
}
