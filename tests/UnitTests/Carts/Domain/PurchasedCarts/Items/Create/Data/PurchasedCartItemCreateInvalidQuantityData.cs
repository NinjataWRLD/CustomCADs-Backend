namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidQuantityData : PurchasedCartItemCreateData
{
    public PurchasedCartItemCreateInvalidQuantityData()
    {
        Add(PurchasedCartsData.ValidId, ValidProductId, ValidCadId, ValidCustomizationId, ValidPrice1, InvalidQuantity1, true);
        Add(PurchasedCartsData.ValidId, ValidProductId, ValidCadId, ValidCustomizationId, ValidPrice2, InvalidQuantity2, false);
    }
}
