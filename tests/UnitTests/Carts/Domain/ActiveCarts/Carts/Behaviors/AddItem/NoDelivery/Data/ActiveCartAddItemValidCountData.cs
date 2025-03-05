namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.NoDelivery.Data;

using static ActiveCartsData;

public class ActiveCartAddItemValidCountData : TheoryData<int>
{
    public ActiveCartAddItemValidCountData()
    {
        Add(ValidItemsCount1);
        Add(ValidItemsCount2);
    }
}
