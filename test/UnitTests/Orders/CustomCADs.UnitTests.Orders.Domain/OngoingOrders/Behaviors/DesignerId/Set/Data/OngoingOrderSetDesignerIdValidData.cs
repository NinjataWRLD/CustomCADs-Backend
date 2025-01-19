namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.DesignerId.Set.Data;

using static OngoingOrdersData;

public class OngoingOrderSetDesignerIdValidData : OngoingOrderSetShipmentIdData
{
    public OngoingOrderSetDesignerIdValidData()
    {
        Add(ValidDesignerId1);
        Add(ValidDesignerId2);
    }
}
