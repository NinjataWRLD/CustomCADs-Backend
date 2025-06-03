namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateValidData : PurchasedCartItemCreateData
{
    public PurchasedCartItemCreateValidData()
    {
        Add(ValidPrice1, ValidQuantity1, true);
        Add(ValidPrice2, ValidQuantity2, false);
    }
}
