namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidQuantityData : PurchasedCartItemCreateData
{
	public PurchasedCartItemCreateInvalidQuantityData()
	{
		Add(MinValidPrice, MinInvalidQuantity, true);
		Add(MaxValidPrice, MaxInvalidQuantity, false);
	}
}
