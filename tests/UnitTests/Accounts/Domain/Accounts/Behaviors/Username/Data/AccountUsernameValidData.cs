namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username.Data;

using static AccountsData;

public class AccountUsernameValidData : AccountUsernameData
{
	public AccountUsernameValidData()
	{
		Add(ValidUsername);
		Add(MinValidUsername);
		Add(MaxValidUsername);
	}
}
