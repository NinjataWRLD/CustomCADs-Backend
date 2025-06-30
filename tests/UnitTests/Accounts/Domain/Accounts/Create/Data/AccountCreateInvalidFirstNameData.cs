namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Data;

using static AccountsData;

public class AccountCreateInvalidFirstNameData : AccountCreateData
{
	public AccountCreateInvalidFirstNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, MinInvalidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, MaxInvalidFirstName, ValidLastNameNull);
	}
}
