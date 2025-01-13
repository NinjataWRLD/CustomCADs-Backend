namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.WithId.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateWithIdInvalidPriceData : PurchasedCartItemCreateWithIdData
{
    public PurchasedCartItemCreateWithIdInvalidPriceData()
    {
        Add(ValidId1, PurchasedCartsData.ValidId2, ValidProductId2, ValidCadId2, InvalidPrice2, ValidQuantity2, false);
        Add(ValidId2, PurchasedCartsData.ValidId1, ValidProductId1, ValidCadId1, InvalidPrice1, ValidQuantity1, true);
    }
}
