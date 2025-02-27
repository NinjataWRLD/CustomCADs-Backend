using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.SetForDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.SetForDelivery;

public class ActiveCartItemSetForDeliveryUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemSetForDeliveryValidData))]
    public void SetForDelivery_ShouldNotThrow(bool forDelivery)
    {
        CreateItem().SetForDelivery(forDelivery);
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemSetForDeliveryValidData))]
    public void SetForDelivery_ShouldPopulateProperly(bool forDelivery)
    {
        var item = CreateItem();
        item.SetForDelivery(forDelivery);
        Assert.Equal(forDelivery, item.ForDelivery);
    }
}
