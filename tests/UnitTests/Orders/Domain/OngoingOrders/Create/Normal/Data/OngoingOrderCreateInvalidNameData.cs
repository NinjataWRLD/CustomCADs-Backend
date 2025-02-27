namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.Normal.Data;

using static OngoingOrdersData;

public class OngoingOrderCreateInvalidNameData : OngoingOrderCreateData
{
    public OngoingOrderCreateInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, true, ValidBuyerId1);
        Add(InvalidName2, ValidDescription2, false, ValidBuyerId2);
    }
}
