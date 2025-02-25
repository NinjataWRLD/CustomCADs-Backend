namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.WithId.Data;

using static OngoingOrdersData;

public class OngoingOrderCreateWithIdValidData : OngoingOrderCreateWithIdData
{
    public OngoingOrderCreateWithIdValidData()
    {
        Add(ValidId1, ValidName1, ValidDescription1, true, ValidBuyerId1);
        Add(ValidId2, ValidName2, ValidDescription2, false, ValidBuyerId2);
    }
}
