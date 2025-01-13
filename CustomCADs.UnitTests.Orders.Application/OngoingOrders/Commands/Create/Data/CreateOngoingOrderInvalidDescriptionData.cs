namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Create.Data;

using static OngoingOrdersData;

public class CreateOngoingOrderInvalidDescriptionData : CreateOngoingOrderData
{
    public CreateOngoingOrderInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, false, ValidBuyerId1);
        Add(ValidName2, InvalidDescription2, true, ValidBuyerId2);
    }
}
