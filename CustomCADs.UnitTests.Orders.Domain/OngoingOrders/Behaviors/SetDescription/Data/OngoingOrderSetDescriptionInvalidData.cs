namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetDescription.Data;

using static OngoingOrdersData;

public class OngoingOrderSetDescriptionInvalidData : OngoingOrderSetDescriptionData
{
    public OngoingOrderSetDescriptionInvalidData()
    {
        Add(InvalidDescription1);
        Add(InvalidDescription2);
    }
}