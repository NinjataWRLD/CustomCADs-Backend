namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Edit;
using static OngoingOrdersData;

public class EditOngoingOrderInvalidNameData : EditOngoingOrderData
{
    public EditOngoingOrderInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
    }
}
