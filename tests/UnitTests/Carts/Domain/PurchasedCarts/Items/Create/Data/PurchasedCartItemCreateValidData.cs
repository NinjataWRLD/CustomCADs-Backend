namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create;
using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateValidData : PurchasedCartItemCreateData
{
	public PurchasedCartItemCreateValidData()
	{
		Add(PurchasedCartsData.ValidId1, ValidProductId1, ValidCadId1, ValidCustomizationId1, ValidPrice1, ValidQuantity1, true);
		Add(PurchasedCartsData.ValidId2, ValidProductId2, ValidCadId2, ValidCustomizationId2, ValidPrice2, ValidQuantity2, false);
	}
}
