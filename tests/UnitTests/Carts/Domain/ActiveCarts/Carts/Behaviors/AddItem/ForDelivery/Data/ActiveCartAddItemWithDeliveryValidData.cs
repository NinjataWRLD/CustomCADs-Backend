namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.ForDelivery.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartAddItemWithDeliveryValidData : ActiveCartAddItemData
{
    public ActiveCartAddItemWithDeliveryValidData()
    {
        Add(ValidProductId1, ValidCustomizationId1);
        Add(ValidProductId2, ValidCustomizationId2);
    }
}
