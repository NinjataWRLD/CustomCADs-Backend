namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal.Data;

using static AccountsData;

public class AccountCreateInvalidLastNameData : AccountCreateData
{
	public AccountCreateInvalidLastNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, MinInvalidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, MaxInvalidLastName);
	}
}
