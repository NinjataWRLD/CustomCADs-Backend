namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetDescription.Data;

using static OngoingOrdersData;

public class OngoingOrderSetDescriptionValidData : OngoingOrderSetDescriptionData
{
    public OngoingOrderSetDescriptionValidData()
    {
        Add(ValidDescription1);
        Add(ValidDescription2);
    }
}
