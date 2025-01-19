namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateInvalidPriceData :  CompletedOrderCreateData
{
    public CompletedOrderCreateInvalidPriceData()
    {
        Add(ValidName1, ValidDescription1, InvalidPrice1, true, ValidOrderDate1, ValidBuyerId1);
        Add(ValidName2, ValidDescription2, InvalidPrice2, false, ValidOrderDate2, ValidBuyerId2);
    }
}
