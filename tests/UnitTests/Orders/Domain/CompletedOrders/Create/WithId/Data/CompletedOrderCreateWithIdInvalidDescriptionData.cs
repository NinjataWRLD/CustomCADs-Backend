namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdInvalidDescriptionData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdInvalidDescriptionData()
    {
        Add(ValidId1, ValidName1, InvalidDescription1, ValidPrice1, true, ValidOrderedAt1, ValidBuyerId1);
        Add(ValidId2, ValidName2, InvalidDescription2, ValidPrice2, false, ValidOrderedAt2, ValidBuyerId2);
    }
}
