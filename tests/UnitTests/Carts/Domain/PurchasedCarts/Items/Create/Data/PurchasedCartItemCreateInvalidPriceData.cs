namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateInvalidPriceData : PurchasedCartItemCreateData
{
    public PurchasedCartItemCreateInvalidPriceData()
    {
        Add(InvalidPrice2, ValidQuantity2, false);
        Add(InvalidPrice1, ValidQuantity1, true);
    }
}
