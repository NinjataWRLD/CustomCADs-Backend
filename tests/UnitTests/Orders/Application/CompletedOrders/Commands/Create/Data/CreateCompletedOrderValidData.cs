namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create.Data;

using static CompletedOrdersData;

public class CreateCompletedOrderValidData : CreateCompletedOrderData
{
    public CreateCompletedOrderValidData()
    {
        Add(ValidName1, ValidDescription1, ValidPrice1, false, ValidOrderDate1, ValidBuyerId1, ValidDesignerId1, ValidCadId1, null);
        Add(ValidName2, ValidDescription2, ValidPrice2, true, ValidOrderDate2, ValidBuyerId2, ValidDesignerId2, ValidCadId2, ValidCustomizationId2);
    }
}
