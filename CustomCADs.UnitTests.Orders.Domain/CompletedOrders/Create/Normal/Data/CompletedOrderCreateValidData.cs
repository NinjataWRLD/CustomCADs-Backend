namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateValidData : CompletedOrderCreateData
{
    public CompletedOrderCreateValidData()
    {
        Add(ValidName1, ValidDescription1, true, ValidOrderDate1, ValidBuyerId1);
        Add(ValidName2, ValidDescription2, false, ValidOrderDate2, ValidBuyerId2);
    }
}
