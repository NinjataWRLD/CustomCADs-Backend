namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Internal.Create;
using static CompletedOrdersData;

public class CreateCompletedOrderValidData : CreateCompletedOrderData
{
    public CreateCompletedOrderValidData()
    {
        Add(ValidName1, ValidDescription1, ValidPrice1, false, ValidOrderedAt1, ValidBuyerId1, ValidDesignerId1, ValidCadId1, null);
        Add(ValidName2, ValidDescription2, ValidPrice2, true, ValidOrderedAt2, ValidBuyerId2, ValidDesignerId2, ValidCadId2, ValidCustomizationId2);
    }
}
