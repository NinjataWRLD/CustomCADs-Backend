namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Behaviors.SetShipmentId.Data;

using static CompletedOrdersData;

public class CompletedOrderSetShipmentIdValidData : CompletedOrderSetShipmentIdData
{
    public CompletedOrderSetShipmentIdValidData()
    {
        Add(ValidShipmentId1);
        Add(ValidShipmentId2);
    }
}
