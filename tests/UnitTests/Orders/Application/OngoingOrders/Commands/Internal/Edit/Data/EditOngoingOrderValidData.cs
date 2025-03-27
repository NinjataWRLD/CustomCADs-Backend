namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Edit;
using static OngoingOrdersData;

public class EditOngoingOrderValidData : EditOngoingOrderData
{
    public EditOngoingOrderValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
    }
}
