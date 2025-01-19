namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.WithId.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateWithIdValidData : PurchasedCartItemCreateWithIdData
{
    public PurchasedCartItemCreateWithIdValidData()
    {
        Add(ValidId1, PurchasedCartsData.ValidId1, ValidProductId1, ValidCadId1, ValidPrice1, ValidQuantity1, true);
        Add(ValidId2, PurchasedCartsData.ValidId2, ValidProductId2, ValidCadId2, ValidPrice2, ValidQuantity2, false);
    }
}
