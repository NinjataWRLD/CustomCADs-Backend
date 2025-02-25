namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetName.Data;

using static OngoingOrdersData;

public class OngoingOrderSetNameValidData : OngoingOrderSetNameData
{
    public OngoingOrderSetNameValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
    }
}
