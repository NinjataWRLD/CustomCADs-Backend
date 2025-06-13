namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidFirstNameData : AccountCreateWithIdData
{
	public AccountCreateWithIdInvalidFirstNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, MinInvalidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, MaxInvalidFirstName, ValidLastNameNull);
	}
}
