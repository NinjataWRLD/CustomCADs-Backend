﻿namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Create.Data;

using static AccountsData;

public class CreateAccountHandlerInvalidUsernameData : CreateAccountHandlerData
{
    public CreateAccountHandlerInvalidUsernameData()
    {
        Add(RolesData.ValidName1, InvalidUsername1, ValidEmail1, ValidTimeZone1, ValidPassword, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, InvalidUsername2, ValidEmail2, ValidTimeZone2, ValidPassword, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, InvalidUsername3, ValidEmail3, ValidTimeZone1, ValidPassword, ValidFirstName1, ValidLastName1);
    }
}
