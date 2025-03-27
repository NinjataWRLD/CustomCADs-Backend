namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateInvalidNameData : CompletedOrderCreateData
{
    public CompletedOrderCreateInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, ValidPrice1, true, ValidOrderedAt1, ValidBuyerId1);
        Add(InvalidName2, ValidDescription2, ValidPrice1, false, ValidOrderedAt2, ValidBuyerId2);
    }
}
