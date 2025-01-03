namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetByUsername.Data;

using static AccountsData;

public class GetAccountByUsernameValidData : GetAccountByUsernameData
{
    public GetAccountByUsernameValidData()
    {
        Add(ValidUsername1);
        Add(ValidUsername2);
        Add(ValidUsername3);
        Add(ValidUsername4);
    }
}
