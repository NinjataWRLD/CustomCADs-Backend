namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdInvalidNameData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdInvalidNameData()
    {
        Add(ValidId1, InvalidName1, ValidDescription1, ValidPrice1, true, ValidOrderedAt1, ValidBuyerId1);
        Add(ValidId2, InvalidName2, ValidDescription2, ValidPrice2, false, ValidOrderedAt2, ValidBuyerId2);
    }
}
