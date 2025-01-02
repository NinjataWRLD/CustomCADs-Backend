﻿namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands.Create.Data;

using static AccountsData;

public class CreateAccountHandlerInvalidFirstNameData : CreateAccountHandlerData
{
    public CreateAccountHandlerInvalidFirstNameData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidTimeZone1, InvalidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidTimeZone2, InvalidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, ValidUsername3, ValidEmail3, ValidTimeZone1, InvalidFirstName1, ValidLastName1);
        Add(RolesData.ValidName4, ValidUsername4, ValidEmail4, ValidTimeZone2, InvalidFirstName2, ValidLastName2);
    }
}
