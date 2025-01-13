namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Edit.Data;

using static OngoingOrdersData;

public class EditOngoingOrderInvalidNameData : EditOngoingOrderData
{
    public EditOngoingOrderInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
    }
}
