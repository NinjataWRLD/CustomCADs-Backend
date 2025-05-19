namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items.Data;

using CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;
using static PurchasedCartsData;

public class CountPurchasedCartItemsValidData : CountPurchasedCartItemsData
{
	public CountPurchasedCartItemsValidData()
	{
		Add(ValidBuyerId1);
		Add(ValidBuyerId2);
	}
}
