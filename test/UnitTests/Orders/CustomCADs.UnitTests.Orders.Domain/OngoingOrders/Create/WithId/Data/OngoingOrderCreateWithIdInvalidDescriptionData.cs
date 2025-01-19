namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.WithId.Data;

using static OngoingOrdersData;

public class OngoingOrderCreateWithIdInvalidDescriptionData : OngoingOrderCreateWithIdData
{
    public OngoingOrderCreateWithIdInvalidDescriptionData()
    {
        Add(ValidId1, ValidName1, InvalidDescription1, true, ValidBuyerId1);
        Add(ValidId2, ValidName2, InvalidDescription2, false, ValidBuyerId2);
    }
}
