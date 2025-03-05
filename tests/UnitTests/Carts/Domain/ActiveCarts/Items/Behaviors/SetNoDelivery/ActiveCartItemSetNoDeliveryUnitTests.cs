namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.SetNoDelivery;

public class ActiveCartItemSetNoDeliveryUnitTests : ActiveCartItemsBaseUnitTests
{
    [Fact]
    public void SetForDelivery_ShouldNotThrow()
    {
        CreateItem().SetNoDelivery();
    }

    [Fact]
    public void SetForDelivery_ShouldPopulateProperly()
    {
        var item = CreateItem();
        item.SetNoDelivery();
        Assert.False(item.ForDelivery);
    }
}
