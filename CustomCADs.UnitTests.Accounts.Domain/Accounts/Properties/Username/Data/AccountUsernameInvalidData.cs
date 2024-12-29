﻿namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.Username.Data;

using static AccountsData;

public class AccountUsernameInvalidData : AccountUsernameData
{
    public AccountUsernameInvalidData()
    {
        Add(InvalidUsername1);
        Add(InvalidUsername2);
        Add(InvalidUsername3);
    }
}
