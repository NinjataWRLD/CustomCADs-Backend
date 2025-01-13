namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdInvalidDescriptionData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdInvalidDescriptionData()
    {
        Add(ValidId1, ValidName1, InvalidDescription1, true, ValidOrderDate1, ValidBuyerId1);
        Add(ValidId2, ValidName2, InvalidDescription2, false, ValidOrderDate2, ValidBuyerId2);
    }
}
