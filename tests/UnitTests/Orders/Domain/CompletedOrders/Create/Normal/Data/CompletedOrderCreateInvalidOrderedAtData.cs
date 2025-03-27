namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateInvalidOrderedAtData : CompletedOrderCreateData
{
    public CompletedOrderCreateInvalidOrderedAtData()
    {
        Add(ValidName1, ValidDescription1, ValidPrice1, true, InvalidOrderedAt1, ValidBuyerId1);
        Add(ValidName2, ValidDescription2, ValidPrice1, false, InvalidOrderedAt2, ValidBuyerId2);
    }
}
