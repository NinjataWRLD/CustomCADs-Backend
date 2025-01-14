namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdInvalidPriceData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdInvalidPriceData()
    {
        Add(ValidId1, ValidName1, ValidDescription1, InvalidPrice1, true, ValidOrderDate1, ValidBuyerId1);
        Add(ValidId2, ValidName2, ValidDescription2, InvalidPrice2, false, ValidOrderDate2, ValidBuyerId2);
    }
}
