namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidPriceData : PurchasedCartItemCreateData
{
	public PurchasedCartItemCreateInvalidPriceData()
	{
		Add(MaxInvalidPrice, MaxValidQuantity, false);
		Add(MinInvalidPrice, MinValidQuantity, true);
	}
}
