using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetCadId.Data;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetCadId;

using static OngoingOrdersData;

public class OngoingOrderSetShipmentIdUnitTests : OngoingOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(OngoingOrderSetCadIdValidData))]
    public void SetDesignerId_ShouldNotThrowException_WhenOrderValid(CadId cadId)
    {
        CreateOrder().SetDesignerId(ValidDesignerId1).SetCadId(cadId);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetCadIdValidData))]
    public void SetDesignerId_ShouldPopulateProperly(CadId cadId)
    {
        var order = CreateOrder();
        order.SetDesignerId(ValidDesignerId1);
        order.SetCadId(cadId);
        Assert.Equal(cadId, order.CadId);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetCadIdValidData))]
    public void SetDesignerId_ShouldThrowException_WhenOrderInvalid(CadId cadId)
    {
        Assert.Throws<CustomValidationException<OngoingOrder>>(() =>
        {
            CreateOrder().SetCadId(cadId);
        });
    }
}
