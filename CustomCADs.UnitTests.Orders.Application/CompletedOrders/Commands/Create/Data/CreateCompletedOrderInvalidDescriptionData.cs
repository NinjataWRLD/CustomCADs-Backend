﻿namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create.Data;

using static CompletedOrdersData;

public class CreateCompletedOrderInvalidDescriptionData : CreateCompletedOrderData
{
    public CreateCompletedOrderInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, ValidPrice1, false, ValidOrderDate1, ValidBuyerId1, ValidDesignerId1, ValidCadId1);
        Add(ValidName2, InvalidDescription2, ValidPrice2, true, ValidOrderDate2, ValidBuyerId2, ValidDesignerId2, ValidCadId2);
    }
}
