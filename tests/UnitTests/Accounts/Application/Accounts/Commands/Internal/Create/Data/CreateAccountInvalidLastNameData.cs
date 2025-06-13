namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using static AccountsData;

public class CreateAccountInvalidLastNameData : CreateAccountData
{
	public CreateAccountInvalidLastNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidPassword, ValidFirstName, MinInvalidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidPassword, ValidFirstNameNull, MaxInvalidLastName);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidPassword, ValidFirstName, MinInvalidLastName);
	}
}
