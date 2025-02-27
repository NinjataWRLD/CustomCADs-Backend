namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create.Data;

using static CompletedOrdersData;

public class CreateCompletedOrderInvalidPriceData : CreateCompletedOrderData
{
    public CreateCompletedOrderInvalidPriceData()
    {
        Add(ValidName1, ValidDescription1, InvalidPrice1, false, ValidOrderDate1, ValidBuyerId1, ValidDesignerId1, ValidCadId1);
        Add(ValidName2, ValidDescription2, InvalidPrice2, true, ValidOrderDate2, ValidBuyerId2, ValidDesignerId2, ValidCadId2);
    }
}
