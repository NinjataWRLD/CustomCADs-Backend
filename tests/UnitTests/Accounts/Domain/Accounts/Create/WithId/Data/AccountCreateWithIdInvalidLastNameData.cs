namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidLastNameData : AccountCreateWithIdData
{
	public AccountCreateWithIdInvalidLastNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, MinInvalidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, MaxInvalidLastName);
	}
}
