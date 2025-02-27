using CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Behaviors.SetShipmentId.Data;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Behaviors.SetShipmentId;

public class CompletedOrderSetShipmentIdUnitTests : CompletedOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CompletedOrderSetShipmentIdValidData))]
    public void SetShipmentId_ShouldNotThrowException_WhenOrderHasDelivery(ShipmentId shipmentId)
    {
        CreateOrder(delivery: true).SetShipmentId(shipmentId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderSetShipmentIdValidData))]
    public void SetShipmentId_ShouldPopulateProperly(ShipmentId shipmentId)
    {
        var order = CreateOrder(delivery: true);
        order.SetShipmentId(shipmentId);
        Assert.Equal(shipmentId, order.ShipmentId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderSetShipmentIdValidData))]
    public void SetShipmentId_ShouldThrowException_WhenOrderDoesNotHasDelivery(ShipmentId shipmentId)
    {
        Assert.Throws<CompletedOrderValidationException>(() =>
        {
            CreateOrder(delivery: false).SetShipmentId(shipmentId);
        });
    }
}
