namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateInvalidNameData : CompletedOrderCreateData
{
    public CompletedOrderCreateInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, true, ValidOrderDate1, ValidBuyerId1);
        Add(InvalidName2, ValidDescription2, false, ValidOrderDate2, ValidBuyerId2);
    }
}
