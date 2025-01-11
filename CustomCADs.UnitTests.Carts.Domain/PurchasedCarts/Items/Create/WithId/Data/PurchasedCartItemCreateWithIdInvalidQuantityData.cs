namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.WithId.Data;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateWithIdInvalidQuantityData : PurchasedCartItemCreateWithIdData
{
    public PurchasedCartItemCreateWithIdInvalidQuantityData()
    {
        Add(ValidId1, PurchasedCartsData.ValidId1, ValidProductId1, ValidCadId1, ValidPrice1, InvalidQuantity1, true);
        Add(ValidId2, PurchasedCartsData.ValidId2, ValidProductId2, ValidCadId2, ValidPrice2, InvalidQuantity2, false);
    }
}
