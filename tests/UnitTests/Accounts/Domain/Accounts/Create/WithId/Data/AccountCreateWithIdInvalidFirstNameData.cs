namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidFirstNameData : AccountCreateWithIdData
{
	public AccountCreateWithIdInvalidFirstNameData()
	{
		Add(ValidId1, RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidFirstName1, ValidLastName1);
		Add(ValidId2, RolesData.ValidName2, ValidUsername2, ValidEmail2, InvalidFirstName2, ValidLastName2);
	}
}
