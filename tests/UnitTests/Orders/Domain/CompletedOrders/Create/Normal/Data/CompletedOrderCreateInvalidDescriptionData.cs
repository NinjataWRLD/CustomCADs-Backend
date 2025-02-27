namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateInvalidDescriptionData : CompletedOrderCreateData
{
    public CompletedOrderCreateInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, ValidPrice1, true, ValidOrderDate1, ValidBuyerId1);
        Add(ValidName2, InvalidDescription2, ValidPrice1, false, ValidOrderDate2, ValidBuyerId2);
    }
}
