namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetPrice.Data;

using static OngoingOrdersData;

public class OngoingOrderSetPriceValidData : OngoingOrderSetPriceData
{
    public OngoingOrderSetPriceValidData()
    {
        Add(ValidPrice1);
        Add(ValidPrice2);
    }
}
