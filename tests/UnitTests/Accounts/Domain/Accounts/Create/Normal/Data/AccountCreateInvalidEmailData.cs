namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal.Data;

using static AccountsData;

public class AccountCreateInvalidEmailData : AccountCreateData
{
	public AccountCreateInvalidEmailData()
	{
		Add(RolesData.ValidName, ValidUsername, InvalidEmail, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, InvalidEmailLocal, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, InvalidEmailDomain, ValidFirstName, ValidLastName);
	}
}
