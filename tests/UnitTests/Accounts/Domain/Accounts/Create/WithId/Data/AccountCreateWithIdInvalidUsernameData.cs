namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidUsernameData : AccountCreateWithIdData
{
	public AccountCreateWithIdInvalidUsernameData()
	{
		Add(RolesData.ValidName, InvalidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinInvalidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxInvalidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
