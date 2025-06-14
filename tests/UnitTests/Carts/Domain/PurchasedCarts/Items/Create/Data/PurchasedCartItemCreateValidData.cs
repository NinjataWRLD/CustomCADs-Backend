namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateValidData : PurchasedCartItemCreateData
{
	public PurchasedCartItemCreateValidData()
	{
		Add(MinValidPrice, MinValidQuantity, true);
		Add(MaxValidPrice, MaxValidQuantity, false);
	}
}
