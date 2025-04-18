﻿namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal.Data;

using static AccountsData;

public class AccountCreateValidData : AccountCreateData
{
    public AccountCreateValidData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, ValidUsername3, ValidEmail3, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName4, ValidUsername4, ValidEmail4, ValidFirstName2, ValidLastName2);
    }
}
