namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetCadId.Data;

using static OngoingOrdersData;

public class OngoingOrderSetCadIdValidData : OngoingOrderSetCadIdData
{
    public OngoingOrderSetCadIdValidData()
    {
        Add(ValidCadId1);
        Add(ValidCadId2);
    }
}
