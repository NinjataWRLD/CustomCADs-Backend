namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdValidData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdValidData()
    {
        Add(ValidId1, ValidName1, ValidDescription1, ValidPrice1, true, ValidOrderDate1, ValidBuyerId1);
        Add(ValidId2, ValidName2, ValidDescription2, ValidPrice2, false, ValidOrderDate2, ValidBuyerId2);
    }
}
