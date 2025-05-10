namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidPriceData : PurchasedCartItemCreateData
{
    public PurchasedCartItemCreateInvalidPriceData()
    {
        Add(PurchasedCartsData.ValidId, ValidProductId, ValidCadId, ValidCustomizationId, InvalidPrice2, ValidQuantity2, false);
        Add(PurchasedCartsData.ValidId, ValidProductId, ValidCadId, ValidCustomizationId, InvalidPrice1, ValidQuantity1, true);
    }
}
