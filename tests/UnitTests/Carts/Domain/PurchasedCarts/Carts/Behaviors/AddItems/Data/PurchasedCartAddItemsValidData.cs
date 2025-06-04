namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.AddItems.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartAddItemsValidData : PurchasedCartAddItemsData
{
	public PurchasedCartAddItemsValidData()
	{
		Add(ValidPrice1);
		Add(ValidPrice2);
	}
}
