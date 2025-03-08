namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.ForDelivery.Data;

using static ActiveCartsData;

public class ActiveCartAddItemWithDeliveryValidCountData : TheoryData<int>
{
    public ActiveCartAddItemWithDeliveryValidCountData()
    {
        Add(ValidItemsCount1);
        Add(ValidItemsCount2);
    }
}
