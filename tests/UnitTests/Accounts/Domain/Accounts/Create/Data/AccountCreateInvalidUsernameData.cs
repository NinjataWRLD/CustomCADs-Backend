namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Data;

using static AccountsData;

public class AccountCreateInvalidUsernameData : AccountCreateData
{
	public AccountCreateInvalidUsernameData()
	{
		Add(RolesData.ValidName, InvalidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinInvalidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxInvalidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
