namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.NoDelivery.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartAddItemValidData : ActiveCartAddItemData
{
    public ActiveCartAddItemValidData()
    {
        Add(ValidProductId1);
        Add(ValidProductId2);
    }
}
