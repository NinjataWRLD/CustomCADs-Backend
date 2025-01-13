﻿namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create.Data;

using static CompletedOrdersData;

public class CreateCompletedOrderValidData : CreateCompletedOrderData
{
    public CreateCompletedOrderValidData()
    {
        Add(ValidName1, ValidDescription1, false, ValidOrderDate1, ValidBuyerId1, ValidDesignerId1, ValidCadId1);
        Add(ValidName2, ValidDescription2, true, ValidOrderDate2, ValidBuyerId2, ValidDesignerId2, ValidCadId2);
    }
}
