namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName.Data;

using static AccountsData;

public class AccountLastNameInvalidData : AccountLastNameData
{
	public AccountLastNameInvalidData()
	{
		Add(InvalidLastName1);
		Add(InvalidLastName2);
	}
}
