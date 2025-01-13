namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateInvalidOrderDateData : CompletedOrderCreateData
{
    public CompletedOrderCreateInvalidOrderDateData()
    {
        Add(ValidName1, ValidDescription1, true, InvalidOrderDate1, ValidBuyerId1);
        Add(ValidName2, ValidDescription2, false, InvalidOrderDate2, ValidBuyerId2);
    }
}
