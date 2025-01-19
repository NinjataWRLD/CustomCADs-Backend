namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.WithId.Data;

using static OngoingOrdersData;

public class OngoingOrderCreateWithIdInvalidNameData : OngoingOrderCreateWithIdData
{
    public OngoingOrderCreateWithIdInvalidNameData()
    {
        Add(ValidId1, InvalidName1, ValidDescription1, true, ValidBuyerId1);
        Add(ValidId2, InvalidName2, ValidDescription2, false, ValidBuyerId2);
    }
}
