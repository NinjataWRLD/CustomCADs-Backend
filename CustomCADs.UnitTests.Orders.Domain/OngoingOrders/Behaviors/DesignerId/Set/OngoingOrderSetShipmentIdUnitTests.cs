using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.DesignerId.Set.Data;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.DesignerId.Set;

public class OngoingOrderSetShipmentIdUnitTests : OngoingOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(OngoingOrderSetDesignerIdValidData))]
    public void SetDesignerId_ShouldNotThrowException(AccountId designerId)
    {
        CreateOrder().SetDesignerId(designerId);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetDesignerIdValidData))]
    public void SetDesignerId_ShouldPopulateProperly(AccountId designerId)
    {
        var order = CreateOrder();
        order.SetDesignerId(designerId);
        Assert.Equal(designerId, order.DesignerId);
    }
}
