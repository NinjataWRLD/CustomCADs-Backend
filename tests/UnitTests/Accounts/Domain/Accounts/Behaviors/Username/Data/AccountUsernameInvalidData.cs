namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username.Data;

using static AccountsData;

public class AccountUsernameInvalidData : AccountUsernameData
{
	public AccountUsernameInvalidData()
	{
		Add(InvalidUsername);
		Add(MinInvalidUsername);
		Add(MaxInvalidUsername);
	}
}
