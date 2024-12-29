﻿namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Data;

using static AccountsData;

public class AccountCreateInvalidLastNameData : AccountCreateData
{
    public AccountCreateInvalidLastNameData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidTimeZone1, ValidFirstName1, InvalidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidTimeZone2, ValidFirstName2, InvalidLastName2);
    }
}
