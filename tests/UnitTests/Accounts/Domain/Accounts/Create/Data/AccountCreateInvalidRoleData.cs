namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Data;

using static AccountsData;

public class AccountCreateInvalidRoleData : AccountCreateData
{
	public AccountCreateInvalidRoleData()
	{
		Add(RolesData.InvalidName, ValidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinInvalidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxInvalidName, MaxValidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
