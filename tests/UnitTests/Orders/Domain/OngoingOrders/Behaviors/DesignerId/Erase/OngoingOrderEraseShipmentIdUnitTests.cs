namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.DesignerId.Erase;

public class OngoingOrderEraseShipmentIdUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public void SetDesignerId_ShouldNotThrowException()
    {
        CreateOrder().EraseDesignerId();
    }

    [Fact]
    public void SetDesignerId_ShouldPopulateProperly()
    {
        var order = CreateOrder();
        order.EraseDesignerId();
        Assert.Null(order.DesignerId);
    }
}
