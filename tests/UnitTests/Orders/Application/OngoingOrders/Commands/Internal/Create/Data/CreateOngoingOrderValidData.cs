namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Create;
using static OngoingOrdersData;

public class CreateOngoingOrderValidData : CreateOngoingOrderData
{
    public CreateOngoingOrderValidData()
    {
        Add(ValidName1, ValidDescription1, false, ValidBuyerId1);
        Add(ValidName2, ValidDescription2, true, ValidBuyerId2);
    }
}
