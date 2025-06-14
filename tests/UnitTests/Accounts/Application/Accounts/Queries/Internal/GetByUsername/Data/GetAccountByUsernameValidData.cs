namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetByUsername.Data;

using static AccountsData;

public class GetAccountByUsernameValidData : GetAccountByUsernameData
{
	public GetAccountByUsernameValidData()
	{
		Add(ValidUsername);
		Add(MinValidUsername);
		Add(MaxValidUsername);
	}
}
