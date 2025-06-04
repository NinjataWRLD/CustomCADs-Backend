namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidQuantityData : PurchasedCartItemCreateData
{
	public PurchasedCartItemCreateInvalidQuantityData()
	{
		Add(ValidPrice1, InvalidQuantity1, true);
		Add(ValidPrice2, InvalidQuantity2, false);
	}
}
