﻿namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create.Data;

using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create;
using static CustomsData;

public class CreateCustomValidData : CreateCustomData
{
    public CreateCustomValidData()
    {
        Add(ValidName1, ValidDescription1, false, ValidBuyerId1);
        Add(ValidName2, ValidDescription2, true, ValidBuyerId2);
    }
}
