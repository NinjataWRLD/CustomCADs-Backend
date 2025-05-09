﻿namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal.Data;

using static AccountsData;

public class AccountCreateInvalidEmailData : AccountCreateData
{
    public AccountCreateInvalidEmailData()
    {
        Add(RolesData.ValidName1, ValidUsername1, InvalidEmail1, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, InvalidEmail2, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, ValidUsername3, InvalidEmail3, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName4, ValidUsername4, InvalidEmail4, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName4, ValidUsername4, InvalidEmail5, ValidFirstName2, ValidLastName2);
    }
}
