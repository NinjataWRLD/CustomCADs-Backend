namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName.Data;

using static AccountsData;

public class AccountLastNameValidData : AccountLastNameData
{
	public AccountLastNameValidData()
	{
		Add(ValidLastName);
		Add(ValidLastNameNull);
	}
}
