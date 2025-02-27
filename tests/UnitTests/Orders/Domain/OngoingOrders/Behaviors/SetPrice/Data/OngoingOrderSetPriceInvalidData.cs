namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetPrice.Data;

using static OngoingOrdersData;

public class OngoingOrderSetPriceInvalidData : OngoingOrderSetPriceData
{
    public OngoingOrderSetPriceInvalidData()
    {
        Add(InvalidPrice1);
        Add(InvalidPrice2);
    }
}