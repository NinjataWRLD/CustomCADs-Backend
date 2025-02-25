namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.Data;

using static ActiveCartsData.CartItemsData;

public class ActiveCartAddItemValidData : ActiveCartAddItemData
{
    public ActiveCartAddItemValidData()
    {
        Add(ValidWeight1, ValidProductId1, false);
        Add(ValidWeight2, ValidProductId2, true);
    }
}
