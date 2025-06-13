namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using static AccountsData;

public class CreateAccountInvalidFirstNameData : CreateAccountData
{
	public CreateAccountInvalidFirstNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidPassword, MinInvalidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidPassword, MaxInvalidFirstName, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidPassword, MinInvalidFirstName, ValidLastName);
	}
}
