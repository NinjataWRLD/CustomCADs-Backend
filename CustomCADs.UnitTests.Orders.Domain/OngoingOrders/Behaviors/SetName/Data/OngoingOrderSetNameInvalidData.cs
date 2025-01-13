namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetName.Data;

using static OngoingOrdersData;

public class OngoingOrderSetNameInvalidData : OngoingOrderSetNameData
{
    public OngoingOrderSetNameInvalidData()
    {
        Add(InvalidName1);
        Add(InvalidName2);
    }
}