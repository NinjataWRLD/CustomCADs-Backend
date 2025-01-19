namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.AddItems.Data;

using static PurchasedCartsData;

public class PurchasedCartAddItemsValidData : PurchasedCartAddItemsData
{
    public PurchasedCartAddItemsValidData()
    {
        Add(CartItemsData.ValidPrice1, CartItemsData.ValidCadId1);
        Add(CartItemsData.ValidPrice2, CartItemsData.ValidCadId2);
    }
}
