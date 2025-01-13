namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdInvalidNameData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdInvalidNameData()
    {
        Add(ValidId1, InvalidName1, ValidDescription1, true, ValidOrderDate1, ValidBuyerId1);
        Add(ValidId2, InvalidName2, ValidDescription2, false, ValidOrderDate2, ValidBuyerId2);
    }
}
