namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateValidData : PurchasedCartItemCreateData
{
    public PurchasedCartItemCreateValidData()
    {
        Add(PurchasedCartsData.ValidId, ValidProductId, ValidCadId, ValidCustomizationId, ValidPrice1, ValidQuantity1, true);
        Add(PurchasedCartsData.ValidId, ValidProductId, ValidCadId, ValidCustomizationId, ValidPrice2, ValidQuantity2, false);
    }
}
