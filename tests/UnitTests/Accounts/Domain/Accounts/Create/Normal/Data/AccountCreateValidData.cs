namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal.Data;

using static AccountsData;

public class AccountCreateValidData : AccountCreateData
{
	public AccountCreateValidData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
