namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.NoDelivery.Data;

using static ActiveCartsData;

public class ActiveCartAddItemInvalidCountData : TheoryData<int>
{
    public ActiveCartAddItemInvalidCountData()
    {
        Add(InvalidItemsCount);
    }
}
