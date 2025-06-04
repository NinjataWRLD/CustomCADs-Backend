namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidRoleData : AccountCreateWithIdData
{
	public AccountCreateWithIdInvalidRoleData()
	{
		Add(RolesData.InvalidName1, ValidUsername1, ValidEmail1, ValidFirstName1, ValidLastName1);
		Add(RolesData.InvalidName2, ValidUsername2, ValidEmail2, ValidFirstName2, ValidLastName2);
		Add(RolesData.InvalidName3, ValidUsername3, ValidEmail3, ValidFirstName1, ValidLastName1);
	}
}
