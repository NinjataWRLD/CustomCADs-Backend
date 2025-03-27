namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdInvalidOrderedAtData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdInvalidOrderedAtData()
    {
        Add(ValidId1, ValidName1, ValidDescription1, ValidPrice1, true, InvalidOrderedAt1, ValidBuyerId1);
        Add(ValidId2, ValidName2, ValidDescription2, ValidPrice2, false, InvalidOrderedAt2, ValidBuyerId2);
    }
}
