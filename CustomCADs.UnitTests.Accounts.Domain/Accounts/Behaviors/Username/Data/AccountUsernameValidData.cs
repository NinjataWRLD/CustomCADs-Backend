namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username.Data;

using static AccountsData;

public class AccountUsernameValidData : AccountUsernameData
{
    public AccountUsernameValidData()
    {
        Add(ValidUsername1);
        Add(ValidUsername2);
        Add(ValidUsername3);
        Add(ValidUsername4);
    }
}
