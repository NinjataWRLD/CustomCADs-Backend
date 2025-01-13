namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

using static CompletedOrdersData;

public class CompletedOrderCreateWithIdInvalidOrderDateData : CompletedOrderCreateWithIdData
{
    public CompletedOrderCreateWithIdInvalidOrderDateData()
    {
        Add(ValidId1, ValidName1, ValidDescription1, true, InvalidOrderDate1, ValidBuyerId1);
        Add(ValidId2, ValidName2, ValidDescription2, false, InvalidOrderDate2, ValidBuyerId2);
    }
}
