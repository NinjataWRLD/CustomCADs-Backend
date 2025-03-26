namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Edit;
using static OngoingOrdersData;

public class EditOngoingOrderInvalidDescriptionData : EditOngoingOrderData
{
    public EditOngoingOrderInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
    }
}
