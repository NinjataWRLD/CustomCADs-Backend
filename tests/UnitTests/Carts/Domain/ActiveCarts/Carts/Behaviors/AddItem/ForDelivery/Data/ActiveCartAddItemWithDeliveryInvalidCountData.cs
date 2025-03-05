namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.ForDelivery.Data;

using static ActiveCartsData;

public class ActiveCartAddItemWithDeliveryInvalidCountData : TheoryData<int>
{
    public ActiveCartAddItemWithDeliveryInvalidCountData()
    {
        Add(InvalidItemsCount);
    }
}
