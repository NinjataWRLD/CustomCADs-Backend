namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Normal.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidPriceData : PurchasedCartItemCreateData
{
    public PurchasedCartItemCreateInvalidPriceData()
    {
        Add(PurchasedCartsData.ValidId2, ValidProductId2, ValidCadId2, InvalidPrice2, ValidQuantity2, false);
        Add(PurchasedCartsData.ValidId1, ValidProductId1, ValidCadId1, InvalidPrice1, ValidQuantity1, true);
    }
}
