namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Normal.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidQuantityData : PurchasedCartItemCreateData
{
    public PurchasedCartItemCreateInvalidQuantityData()
    {
        Add(PurchasedCartsData.ValidId1, ValidProductId1, ValidCadId1, ValidPrice1, InvalidQuantity1, true);
        Add(PurchasedCartsData.ValidId2, ValidProductId2, ValidCadId2, ValidPrice2, InvalidQuantity2, false);
    }
}
