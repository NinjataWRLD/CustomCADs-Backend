namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.Normal.Data;

using static OngoingOrdersData;

public class OngoingOrderCreateInvalidDescriptionData : OngoingOrderCreateData
{
    public OngoingOrderCreateInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, true, ValidBuyerId1);
        Add(ValidName2, InvalidDescription2, false, ValidBuyerId2);
    }
}
