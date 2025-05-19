namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create;
using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidPriceData : PurchasedCartItemCreateData
{
	public PurchasedCartItemCreateInvalidPriceData()
	{
		Add(PurchasedCartsData.ValidId2, ValidProductId2, ValidCadId2, ValidCustomizationId2, InvalidPrice2, ValidQuantity2, false);
		Add(PurchasedCartsData.ValidId1, ValidProductId1, ValidCadId1, ValidCustomizationId1, InvalidPrice1, ValidQuantity1, true);
	}
}
