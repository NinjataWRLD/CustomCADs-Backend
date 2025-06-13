namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using static AccountsData;

public class CreateAccountInvalidLastNameData : CreateAccountData
{
	public CreateAccountInvalidLastNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, MinInvalidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, MaxInvalidLastName);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidFirstName, MinInvalidLastName);
	}
}
