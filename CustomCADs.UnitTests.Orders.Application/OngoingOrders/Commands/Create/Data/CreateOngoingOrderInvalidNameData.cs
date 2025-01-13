﻿namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Create.Data;

using static OngoingOrdersData;

public class CreateOngoingOrderInvalidNameData : CreateOngoingOrderData
{
    public CreateOngoingOrderInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, false, ValidBuyerId1);
        Add(InvalidName2, ValidDescription2, true, ValidBuyerId2);
    }
}
