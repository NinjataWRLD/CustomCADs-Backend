namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.Normal.Data;

using static OngoingOrdersData;

public class OngoingOrderCreateValidData : OngoingOrderCreateData
{
    public OngoingOrderCreateValidData()
    {
        Add(ValidName1, ValidDescription1, true, ValidBuyerId1);
        Add(ValidName2, ValidDescription2, false, ValidBuyerId2);
    }
}
