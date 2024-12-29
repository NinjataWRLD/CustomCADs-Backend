namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetByUsername.Data;

using static AccountsData;

public class GetAccountByUsernameHandlerValidData : GetAccountByUsernameHandlerData
{
    public GetAccountByUsernameHandlerValidData()
    {
        Add(ValidUsername1);
        Add(ValidUsername2);
        Add(ValidUsername3);
        Add(ValidUsername4);
    }
}
